using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	public class UsersController : ODataController
	{
		private readonly IEntityService<User, RegisterUserModel, GetPublicUserModel> _userHelper;
		private readonly IAuthenticatableEntityService<AuthenticateUserRequest, AuthenticateUserResponse, User, GetPrivateUserModel> _userAuthenticator;
		private readonly IUserSaverService _userSaver;

		public UsersController(IEntityService<User, RegisterUserModel, GetPublicUserModel> userHelper, 
			IAuthenticatableEntityService<AuthenticateUserRequest, AuthenticateUserResponse, User, GetPrivateUserModel> userAuthenticator,
			IUserSaverService userSaver)
		{
			_userHelper = userHelper;
			_userAuthenticator = userAuthenticator;
			_userSaver = userSaver;
		}
		#region Authenticate
		[HttpPost]
		public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserRequest model)
		{
			var response = await _userAuthenticator.AuthenticateAsync(model);

			if (response == null) throw new NotFoundException("Username or password is incorrect.");

			return Ok(response);
		}
		#endregion

		#region Create
		[HttpPost("register")]
		public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserModel model)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var publicResponse = await _userHelper.PostEntityModelAsync(model, null);

			return Ok(publicResponse);
		}

		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetUserAsync(int key)
		{
			GetPublicUserModel publicModel = await _userHelper.GetEntityModelAsync(key);

			if (publicModel == null) throw new NotFoundException();

			return Ok(publicModel);
		}

		[EnableQuery]
		public async Task<IActionResult> GetUsersAsync()
		{
			var publicModels = await _userHelper.GetAllEntityModelsAsync();
			return Ok(publicModels);
		}

		[HttpGet("me")]
		[Authorize]
		public async Task<IActionResult> GetPrivateUserAsync()
		{
			var privateUser = await _userAuthenticator.GetAuthenticatedReturnModelAsync(HttpContext);

			if (privateUser == null) return StatusCode(StatusCodes.Status500InternalServerError);

			return Ok(privateUser);
		}
		#endregion

		#region Update
		[EnableQuery]
		[Authorize]
		public async Task<IActionResult> PutUserAsync(int key, [FromBody] RegisterUserModel userModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthenticator.GetAuthenticatedUser(HttpContext);

			var updatedUserModel = await _userHelper.PutEntityModelAsync(key, userModel, user);

			return Ok(updatedUserModel);
		}

		[HttpPost("save-article({articleId:int})")]
		[Authorize]
		public async Task<IActionResult> SaveArticleAsync(int articleId)
		{
			var user = await _userAuthenticator.GetAuthenticatedUser(HttpContext);
			
			user = await _userSaver.SaveArticleToUserAsync(user, articleId);

			return Ok(user);
		}

		[HttpPost("save-comment({commentId:int})")]
		[Authorize]
		public async Task<IActionResult> SaveCommentAsync(int commentId)
		{
			var user = await _userAuthenticator.GetAuthenticatedUser(HttpContext);

			user = await _userSaver.SaveCommentToUserAsync(user, commentId);

			return Ok(user);
		}
		#endregion

		#region Delete
		[HttpDelete("{id:int}")]
		[Authorize]
		public async Task<IActionResult> DeleteUserAsync(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthenticator.GetAuthenticatedUser(HttpContext);

			await _userHelper.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
		#endregion
	}
}
