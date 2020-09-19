using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.EF;
using HackerNews.EF.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class DeveloperController : ControllerBase
	{
		private readonly IEntityRepository<Article> _articleRepository;
		private readonly IEntityRepository<Comment> _commentRepository;
		private readonly HackerNewsContext _context;

		public DeveloperController(IEntityRepository<Article> articleRepository, IEntityRepository<Comment> commentRepository, HackerNewsContext context)
		{						   
			_articleRepository = articleRepository;
			_commentRepository = commentRepository;
			_context = context;
		}

		[HttpOptions("delete-all")]
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
