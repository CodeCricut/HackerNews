using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class DeveloperController : ControllerBase
	{
		private readonly EntityRepository<Article> _articleRepository;
		private readonly EntityRepository<Comment> _commentRepository;
		private readonly DbContext _context;

		public DeveloperController(EntityRepository<Article> articleRepository, EntityRepository<Comment> commentRepository, DbContext context)
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
