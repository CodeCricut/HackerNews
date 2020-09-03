using HackerNews.Api.DB_Helpers;
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
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				var addedModel = await _articleHelper.PostArticleModelAsync(articleModel);

				return Ok(addedModel);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("range")]
		public async Task<IActionResult> PostArticlesAsync([FromBody] List<PostArticleModel> articleModels)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				await _articleHelper.PostArticleModelsAsync(articleModels);

				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetArticlesAsync()
		{
			try
			{
				var articleModels = await _articleHelper.GetAllArticleModelsAsync();
				return Ok(articleModels);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[EnableQuery]
		public async Task<IActionResult> GetArticleAsync(int key)
		{
			try
			{
				var articleModel = await _articleHelper.GetArticleModelAsync(key);

				return Ok(articleModel);
			}
			catch (Exception e)
			{
				// TODO: add invalid id exception
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutArticleAsync(int id, [FromBody] PostArticleModel articleModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				var updatedArticleModel = await _articleHelper.PutArticleModelAsync(id, articleModel);

				return Ok(updatedArticleModel);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("vote/{articleId:int}")]
		public async Task<IActionResult> VoteArticleAsync(int articleId, [FromBody] bool upvote)
		{
			try
			{
				await _articleHelper.VoteArticleAsync(articleId, upvote);

				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
		#endregion

		#region Delete
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteArticleAsync(int key)
		{
			try
			{
				await _articleHelper.DeleteArticleAsync(key);

				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
		#endregion
	}
}
