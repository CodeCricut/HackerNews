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
			ILogger<UsersController> logger) : base(userService, userAuthService, logger)
		{
			_userSaverService = userSaverService;
		}

		#region Authenticate
		/// <summary>
		/// Return an <see cref="AuthenticateUserResponse"/> if the <paramref name="model"/> contains valid login information. 
		/// The <see cref="AuthenticateUserResponse"/> will contain a JWT token valid for 7 days which can be used to make requests to authenticated endpoints.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserRequest model)
		{
			var response = await _userAuthService.AuthenticateAsync(model);

			if (response == null) throw new NotFoundException("Username or password is incorrect.");

			return Ok(response);
		}
		#endregion

		#region Create
		/// <summary>
		/// Register a new user and add it to the database.
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns><see cref="GetPrivateUserModel"/></returns>
		[HttpPost("register")]
		[Authorize(false)]
		public override async Task<IActionResult> Post([FromBody] RegisterUserModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var publicResponse = await _entityService.PostEntityModelAsync(postModel, null);

			// TODO: very messy just for the purpose of returning a get private user model
			await _userAuthService.AuthenticateAsync(
				new AuthenticateUserRequest { Username = postModel.Username, Password = postModel.Password });
			var privateModel = await _userAuthService.GetAuthenticatedReturnModelAsync(HttpContext);

			return Ok(privateModel);
		}

		/// <summary>
		/// Adding multiple users at once is not permitted.
		/// </summary>
		/// <param name="postModels"></param>
		/// <returns></returns>
		[Authorize(false)]
		public override Task<IActionResult> PostRange([FromBody] List<RegisterUserModel> postModels)
		{
			throw new UnauthorizedException("You are not authorized to register multiple users at once.");
		}
		#endregion

		#region Read
		/// <summary>
		/// Get the <see cref="GetPrivateUserModel"/> from the database which represents the current user (stored in the necessary JWT token).
		/// </summary>
		/// <returns></returns>
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
		/// <summary>
		/// Save the article whose Id = <paramref name="articleId"/> to the current user.
		/// </summary>
		/// <param name="articleId"></param>
		/// <returns></returns>
		[HttpPost("save-article")]
		[Authorize]
		public async Task<IActionResult> SaveArticleAsync([FromQuery] int articleId)
		{
			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);
			
			var privateReturnModel = await _userSaverService.SaveArticleToUserAsync(user, articleId);

			return Ok(privateReturnModel);
		}

		/// <summary>
		/// Save the comment whose Id = <paramref name="commentId"/> to the current user.
		/// </summary>
		/// <param name="commentId"></param>
		/// <returns></returns>
		[HttpPost("save-comment")]
		[Authorize]
		public async Task<IActionResult> SaveCommentAsync([FromQuery] int commentId)
		{
			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var privateReturnModel = await _userSaverService.SaveCommentToUserAsync(user, commentId);

			return Ok(privateReturnModel);
		}
		#endregion

		#region Delete
		#endregion
	}
}
