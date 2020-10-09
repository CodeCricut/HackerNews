using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.ArticleControllers
{
	[Route("api/Articles")]
	public class VoteArticleController : VoteEntityController<Article>
	{
		private readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuthService;
		private readonly IVoteableEntityService<Article> _articleVoter;

		public VoteArticleController(IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService,
			IVoteableEntityService<Article> articleVoter)
		{
			_userAuthService = userAuthService;
			_articleVoter = articleVoter;
		}

		public override async Task<IActionResult> VoteEntityAsync(int entityId, [FromBody] bool upvote)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _articleVoter.VoteEntityAsync(entityId, upvote, user);

			return Ok();
		}
	}
}
