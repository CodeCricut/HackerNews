using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.Attributes;
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
		private readonly IUserSaverService _userSaverService;

		public UserSaverController(IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService,
			IUserSaverService userSaverService)
		{
			_userAuthService = userAuthService;
			_userSaverService = userSaverService;
		}

		[HttpPost("save-article")]
		[Authorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveArticleAsync([FromQuery] int articleId)
		{
			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var privateReturnModel = await _userSaverService.SaveArticleToUserAsync(user, articleId);

			return Ok(privateReturnModel);
		}

		[HttpPost("save-comment")]
		[Authorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveCommentAsync([FromQuery] int commentId)
		{
			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var privateReturnModel = await _userSaverService.SaveCommentToUserAsync(user, commentId);

			return Ok(privateReturnModel);
		}
	}
}
