using CleanEntityArchitecture.Authorization;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class UserSaverController : ControllerBase, IUserSaverController
	{
		private readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuthService;
		private readonly IUserAuth<User> _userAuth;
		private readonly IUserSaverService _userSaverService;

		// TODO: refactor or rename omg
		public UserSaverController(IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService, IUserAuth<User> userAuth,
			IUserSaverService userSaverService)
		{
			_userAuthService = userAuthService;
			_userAuth = userAuth;
			_userSaverService = userSaverService;
		}

		[HttpPost("save-article")]
		[Authorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveArticleAsync([FromQuery] int articleId)
		{
			var user = await _userAuth.GetAuthenticatedUserAsync();

			var privateReturnModel = await _userSaverService.SaveArticleToUserAsync(user, articleId);

			return Ok(privateReturnModel);
		}

		[HttpPost("save-comment")]
		[Authorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveCommentAsync([FromQuery] int commentId)
		{
			var user = await _userAuth.GetAuthenticatedUserAsync();

			var privateReturnModel = await _userSaverService.SaveCommentToUserAsync(user, commentId);

			return Ok(privateReturnModel);
		}
	}
}
