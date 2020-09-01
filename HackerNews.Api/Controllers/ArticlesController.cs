using AutoMapper;
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
		private readonly IArticleRepository _articleRepository;
		// private readonly IArticleCommentRepository _artCommRepository;
		private readonly IMapper _mapper;

		public ArticlesController(IArticleRepository articleRepository, IArticleCommentRepository artCommRepository, IMapper mapper)
		{
			_articleRepository = articleRepository;
			// _artCommRepository = artCommRepository;
			_mapper = mapper;
		}


		// good
		public async Task<IActionResult> Get()
		{
			try
			{
				//var articles = (await _artCommRepository.GetArticlesWithChildrenAsync()).ToList();
				//var models = _mapper.Map<IEnumerable<GetArticleModel>>(articles);

				//return Ok(models);

				// actually gets with children
				var articles = (await _articleRepository.GetArticlesWithoutChildrenAsync()).ToList();

				for (int i = 0; i < articles.Count; i++)
				{
					var article = articles[i];
					article = EntityTrimmer.GetNewTrimmedArticle(article, false);
				}

				var models = _mapper.Map<List<GetArticleModel>>(articles);

				return Ok(articles);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		// good
		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				//var article = await _artCommRepository.GetArticleWithChildrenAsync(id);

				var article = (await _articleRepository.GetArticleWithoutChildrenAsync(id));
				article = EntityTrimmer.GetNewTrimmedArticle(article, false);
				var model = _mapper.Map<GetArticleModel>(article);

				return Ok(model);
			}
			catch (Exception e)
			{
				// TODO: add invalid id exception
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}


		[HttpPost]
		public async Task<IActionResult> PostArticle([FromBody] PostArticleModel articleModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				Article article = _mapper.Map<Article>(articleModel);
				_articleRepository.AddArticle(article);
				await _articleRepository.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}


		// good?
		// deletes comments for some reason...
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutArticle(int id, [FromBody] PostArticleModel articleModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				//var article = await _artCommRepository.GetArticleWithChildrenAsync(id); 
				//// this is messy, but a quick fix
				//article.Title = articleModel.Title;
				//article.Text = articleModel.Text;
				//article.Type = (ArticleType) Enum.Parse(typeof(ArticleType), articleModel.Type);
				//article.AuthorName = articleModel.AuthorName;


				//_articleRepository.UpdateArticle(id, article);
				//await _articleRepository.SaveChangesAsync();
				//article = await _artCommRepository.GetArticleWithChildrenAsync(id);

				// doesn't include comments comments, don't know if that is good
				var article = await _articleRepository.GetArticleWithoutChildrenAsync(id);
				// this is messy, but a quick fix
				article.Title = articleModel.Title;
				article.Text = articleModel.Text;
				article.Type = (ArticleType) Enum.Parse(typeof(ArticleType), articleModel.Type);
				article.AuthorName = articleModel.AuthorName;

				_articleRepository.UpdateArticle(id, article);
				await _articleRepository.SaveChangesAsync();

				article = await _articleRepository.GetArticleWithoutChildrenAsync(id);
				article = EntityTrimmer.GetNewTrimmedArticle(article, false);
				var model = _mapper.Map<GetArticleModel>(article);

				return Ok(model);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		// good
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteArticle(int id)
		{
			try
			{
				_articleRepository.DeleteArticle(id);
				await _articleRepository.SaveChangesAsync();
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}


		[HttpPost("vote/{articleId:int}")]
		public async Task<IActionResult> VoteArticle(int articleId, [FromBody] bool upvote)
		{
			try
			{
				//var article = await _artCommRepository.GetArticleWithChildrenAsync(articleId); 
				var article = await _articleRepository.GetArticleWithoutChildrenAsync(articleId);
				if (upvote)
					article.Karma++;
				else article.Karma--;

				_articleRepository.UpdateArticle(articleId, article);
				await _articleRepository.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
