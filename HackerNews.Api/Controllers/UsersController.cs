using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	public class UsersController : EntityCrudController<User, RegisterUserModel, GetPublicUserModel>
	{
		private readonly UserSaverService _userSaverService;

		public UsersController(
			UserService userService,
			UserAuthService userAuthService,
			UserSaverService userSaverService,
			ILogger logger) : base(userService, userAuthService, logger)
		{
			_userSaverService = userSaverService;
		}

		#region Authenticate
		[HttpPost]
		public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserRequest model)
		{
			var response = await _userAuthService.AuthenticateAsync(model);

			if (response == null) throw new NotFoundException("Username or password is incorrect.");

			return Ok(response);
		}
		#endregion

		#region Create
		[HttpPost("register")]
		public override async Task<IActionResult> PostEntityAsync([FromBody] RegisterUserModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var publicResponse = await _entityService.PostEntityModelAsync(postModel, null);

			return Ok(publicResponse);
		}

		public override Task<IActionResult> PostEntitiesAsync([FromBody] List<RegisterUserModel> postModels)
		{
			throw new UnauthorizedException("You are not authorized to register multiple users at once.");
		}
		#endregion

		#region Read
		public override async Task<IActionResult> GetEntitiesAsync()
		{
			var publicModels = await _entityService.GetAllEntityModelsAsync();
			return Ok(publicModels);
		}

		public override async Task<IActionResult> GetEntityAsync(int key)
		{
			GetPublicUserModel publicModel = await _entityService.GetEntityModelAsync(key);

			if (publicModel == null) throw new NotFoundException();

			return Ok(publicModel);
		}

		[HttpGet("me")]
		[Authorize]
		public async Task<IActionResult> GetPrivateUserAsync()
		{
			var privateUser = await _userAuthService.GetAuthenticatedReturnModelAsync(HttpContext);

			if (privateUser == null) return StatusCode(StatusCodes.Status500InternalServerError);

			return Ok(privateUser);
		}
		#endregion

		#region Update
		[Authorize]
		public override async Task<IActionResult> PutEntityAsync(int key, [FromBody] RegisterUserModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedUserModel = await _entityService.PutEntityModelAsync(key, postModel, user);

			return Ok(updatedUserModel);
		}

		[HttpPost("save-article({articleId:int})")]
		[Authorize]
		public async Task<IActionResult> SaveArticleAsync(int articleId)
		{
			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);
			
			user = await _userSaverService.SaveArticleToUserAsync(user, articleId);

			return Ok(user);
		}

		[HttpPost("save-comment({commentId:int})")]
		[Authorize]
		public async Task<IActionResult> SaveCommentAsync(int commentId)
		{
			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			user = await _userSaverService.SaveCommentToUserAsync(user, commentId);

			return Ok(user);
		}
		#endregion

		#region Delete
		[Authorize]
		public override async Task<IActionResult> DeleteEntityAsync(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _entityService.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
		#endregion
	}
}
