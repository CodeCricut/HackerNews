using CleanEntityArchitecture.Authorization;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class UserSaverController : ControllerBase, IUserSaverController
	{
		private readonly IUserAuth<User> _userAuth;
		private readonly IUserSaverService _userSaverService;

		public UserSaverController(IUserAuth<User> userAuth,
			IUserSaverService userSaverService)
		{
			_userAuth = userAuth;
			_userSaverService = userSaverService;
		}

		[HttpPost("save-article")]
		[JwtAuthorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveArticleAsync([FromQuery] int articleId)
		{
			var user = await _userAuth.GetAuthenticatedUserAsync();

			var privateReturnModel = await _userSaverService.SaveArticleToUserAsync(user, articleId);

			return Ok(privateReturnModel);
		}

		[HttpPost("save-comment")]
		[JwtAuthorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveCommentAsync([FromQuery] int commentId)
		{
			var user = await _userAuth.GetAuthenticatedUserAsync();

			var privateReturnModel = await _userSaverService.SaveCommentToUserAsync(user, commentId);

			return Ok(privateReturnModel);
		}
	}
}
