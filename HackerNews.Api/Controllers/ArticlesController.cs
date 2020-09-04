using HackerNews.Api.DB_Helpers;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	// template for non-odata routes
	[Route("api/[controller]")]
	public class ArticlesController : ODataController
	{
		private readonly IArticleHelper _articleHelper;

		public ArticlesController(IArticleHelper articleHelper)
		{
			_articleHelper = articleHelper;
		}

		#region Create
		[HttpPost]
		public async Task<IActionResult> PostArticleAsync([FromBody] PostArticleModel articleModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var addedModel = await _articleHelper.PostArticleModelAsync(articleModel);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		public async Task<IActionResult> PostArticlesAsync([FromBody] List<PostArticleModel> articleModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			await _articleHelper.PostArticleModelsAsync(articleModels);

			return Ok();
		}
		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetArticlesAsync()
		{
			var articleModels = await _articleHelper.GetAllArticleModelsAsync();
			return Ok(articleModels);
		}

		[EnableQuery]
		public async Task<IActionResult> GetArticleAsync(int key)  
		{
				var articleModel = await _articleHelper.GetArticleModelAsync(key);

				return Ok(articleModel);
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutArticleAsync(int id, [FromBody] PostArticleModel articleModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var updatedArticleModel = await _articleHelper.PutArticleModelAsync(id, articleModel);

			return Ok(updatedArticleModel);
		}

		[HttpPost("vote/{articleId:int}")]
		public async Task<IActionResult> VoteArticleAsync(int articleId, [FromBody] bool upvote)
		{
			// verify article exists to throw custom exception if null
			await GetArticleAsync(articleId);

			await _articleHelper.VoteArticleAsync(articleId, upvote);

			return Ok();
		}
		#endregion

		#region Delete
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteArticleAsync(int key)
		{
			await _articleHelper.DeleteArticleAsync(key);

			return Ok();
		}
		#endregion
	}
}
