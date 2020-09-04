using HackerNews.Domain.Errors;
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
	public class DeveloperController : ControllerBase
	{
		private readonly IArticleRepository _articleRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly HackerNewsContext _context;

		public DeveloperController(IArticleRepository articleRepository, ICommentRepository commentRepository, HackerNewsContext context)
		{
			_articleRepository = articleRepository;
			_commentRepository = commentRepository;
			_context = context;
		}

		[HttpOptions]
		public async Task<IActionResult> DeleteAllDataAsync()
		{
			try
			{
				var articles = await _articleRepository.GetArticlesAsync();
				var comments = await _commentRepository.GetCommentsAsync(true);

				_context.RemoveRange(articles);
				_context.RemoveRange(comments);

				await _context.SaveChangesAsync();

				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

		}

		[HttpGet]
		public IActionResult ThrowError()
		
		{
			throw new InvalidPostException(ModelState);

			return Ok();
		}
	}
}
