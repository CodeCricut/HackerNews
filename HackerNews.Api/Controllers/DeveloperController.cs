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
		private readonly ArticleRepository _articleRepository;
		private readonly CommentRepository _commentRepository;
		private readonly HackerNewsContext _context;

		public DeveloperController(ArticleRepository articleRepository, CommentRepository commentRepository, HackerNewsContext context)
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
				var articles = await _articleRepository.GetEntitiesAsync();
				var comments = await _commentRepository.GetEntitiesAsync();

				_context.RemoveRange(articles);
				_context.RemoveRange(comments);

				await _context.SaveChangesAsync();

				return Ok();
			}
			catch 
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

		}

		[HttpGet]
		public IActionResult ThrowError()
		
		{
			throw new InvalidPostException(ModelState);
		}
	}
}
