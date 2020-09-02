using AutoMapper;
using HackerNews.Api.DB_Helpers;
using HackerNews.Api.Profiles;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class ArticlesController : ControllerBase
	{
		private readonly IArticleHelper _articleHelper;

		public ArticlesController(IArticleHelper articleHelper)
		{
			_articleHelper = articleHelper;
		}


		public async Task<IActionResult> Get()
		{
			try
			{
				var articleModels = await _articleHelper.GetAllArticleModels();
				return Ok(articleModels);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var articleModel = await _articleHelper.GetArticleModel(id);

				return Ok(articleModel);
			}
			catch (Exception e)
			{
				// TODO: add invalid id exception
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}


		[HttpPost]
		public IActionResult PostArticle([FromBody] PostArticleModel articleModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				_articleHelper.PostArticleModel(articleModel);

				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}


		// deletes comments for some reason...
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutArticle(int id, [FromBody] PostArticleModel articleModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				var updatedArticleModel = await _articleHelper.PutArticleModel(id, articleModel);

				return Ok(updatedArticleModel);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{id:int}")]
		public IActionResult DeleteArticle(int id)
		{
			try
			{
				_articleHelper.DeleteArticle(id);

				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("vote/{articleId:int}")]
		public IActionResult VoteArticle(int articleId, [FromBody] bool upvote)
		{
			try
			{
				_articleHelper.VoteArticle(articleId, upvote);

				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
