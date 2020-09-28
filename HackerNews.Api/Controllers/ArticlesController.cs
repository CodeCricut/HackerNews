using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	// template for non-odata routes
	[Route("api/[controller]")]
	public class ArticlesController : ODataController
	{
		private readonly IEntityHelper<Article, PostArticleModel, GetArticleModel> _articleHelper;
		private readonly IVoteableEntityHelper<Article> _articleVoter;

		public ArticlesController(IEntityHelper<Article, PostArticleModel, GetArticleModel> articleHelper,
			IVoteableEntityHelper<Article> articleVoter)
		{
			_articleHelper = articleHelper;
			_articleVoter = articleVoter;
		}

		#region Create
		[HttpPost]
		public async Task<IActionResult> PostArticleAsync([FromBody] PostArticleModel articleModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var addedModel = await _articleHelper.PostEntityModelAsync(articleModel);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		public async Task<IActionResult> PostArticlesAsync([FromBody] List<PostArticleModel> articleModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			await _articleHelper.PostEntityModelsAsync(articleModels);

			return Ok();
		}
		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetArticlesAsync()
		{
			var articleModels = await _articleHelper.GetAllEntityModelsAsync();
			return Ok(articleModels);
		}

		[EnableQuery]
		public async Task<IActionResult> GetArticleAsync(int key)
		{
			var articleModel = await _articleHelper.GetEntityModelAsync(key);

			return Ok(articleModel);
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutArticleAsync(int id, [FromBody] PostArticleModel articleModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var updatedArticleModel = await _articleHelper.PutEntityModelAsync(id, articleModel);

			return Ok(updatedArticleModel);
		}

		[HttpPost("vote/{articleId:int}")]
		public async Task<IActionResult> VoteArticleAsync(int articleId, [FromBody] bool upvote)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);
			// TODO: verify article exists to throw custom exception if null
			// await GetArticleAsync(articleId);

			await _articleVoter.VoteEntityAsync(articleId, upvote);

			return Ok();
		}
		#endregion

		#region Delete
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteArticleAsync(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);
			await _articleHelper.SoftDeleteEntityAsync(key);

			return Ok();
		}
		#endregion
	}
}
