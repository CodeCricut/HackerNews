

using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	// template for non-odata routes
	[Route("api/[controller]")]
	public class ArticlesController : EntityCrudController<Article, PostArticleModel, GetArticleModel>
	{
		private readonly IVoteableEntityService<Article> _articleVoter;

		public ArticlesController(
			ArticleService articleService,
			IVoteableEntityService<Article> articleVoter,
			UserAuthService userAuthService,
			ILogger<ArticlesController> logger) : base(articleService, userAuthService, logger)
		{
			_articleVoter = articleVoter;
		}


		#region Create
		#endregion

		#region Read
		#endregion

		#region Update
		[HttpPost("vote/{articleId:int}")]
		[Authorize]
		public async Task<IActionResult> VoteArticleAsync(int articleId, [FromBody] bool upvote)
		{
			_logger.LogInformation("VoteArticleAsync called.");
			if (articleId < 1) _logger.LogWarning("Invalid id provided or id didn't bind.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _articleVoter.VoteEntityAsync(articleId, upvote, user);

			return Ok();
		}
		#endregion

		#region Delete
		#endregion
	}
}
