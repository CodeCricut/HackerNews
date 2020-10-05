

using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Base.ArticleServices;
using HackerNews.Api.Helpers.EntityServices.Base.UserServices;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Parameters;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	// template for non-odata routes
	[Route("api/[controller]")]
	public class ArticlesController : IModifyEntityController<Article, PostArticleModel, GetArticleModel>

	{

		private readonly IVoteableEntityService<Article> _articleVoter;

		public ArticlesController(
			VoteArticleService articleService,
			IVoteableEntityService<Article> articleVoter,
			UserAuthService userAuthService,
			ILogger<ArticlesController> logger)
		{
			_articleVoter = articleVoter;
		}

		#region Create
		#endregion

		#region Read
		#endregion

		#region Update
		/// <summary>
		/// Vote on the <see cref="Article"/> with the Id = <paramref name="articleId"/>. 
		/// </summary>
		/// <param name="articleId"></param>
		/// <param name="upvote">True if upvote, false if downvote</param>
		/// <returns></returns>
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


		public Task Delete(int key)
		{
			throw new System.NotImplementedException();
		}

		public Task<GetArticleModel> PostAsync([FromBody] PostArticleModel postModel)
		{
			throw new System.NotImplementedException();
		}

		public Task PostRangeAsync([FromBody] IEnumerable<PostArticleModel> postModels)
		{
			throw new System.NotImplementedException();
		}

		public Task<GetArticleModel> Put(int key, [FromBody] PostArticleModel updateModel)
		{
			throw new System.NotImplementedException();
		}
	}
}
