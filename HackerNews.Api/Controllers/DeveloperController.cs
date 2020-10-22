using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class DeveloperController : ControllerBase
	{
		private readonly IReadEntityRepository<Article> _articleRepository;
		private readonly IReadEntityRepository<Comment> _commentRepository;
		private readonly IReadEntityRepository<User> _userRepository;
		private readonly IReadEntityRepository<Board> _boardRepository;
		private readonly DbContext _context;

		public DeveloperController(
			 IReadEntityRepository<Article> articleRepository,
			 IReadEntityRepository<Comment> commentRepository,
			 IReadEntityRepository<User> userRepository,
			 IReadEntityRepository<Board> boardRepository,
			DbContext context)
		{
			_articleRepository = articleRepository;
			_commentRepository = commentRepository;
			_userRepository = userRepository;
			_boardRepository = boardRepository;
			_context = context;
		}

		[HttpOptions("delete-all")]
		public async Task<IActionResult> DeleteAllDataAsync()
		{
			try
			{
				var articles = _context.Set<Article>().ToList();
				var comments = _context.Set<Comment>().ToList();
				var users = _context.Set<User>().ToList();
				var boards = _context.Set<Board>().ToList();

				_context.RemoveRange(articles);
				_context.RemoveRange(comments);
				_context.RemoveRange(users);
				_context.RemoveRange(boards);

				await _context.SaveChangesAsync();

				return Ok();
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

		}

		[HttpOptions("get-all")]
		public async Task<IActionResult> GetAll()
		{
			throw new NotImplementedException();
			//var articles = _context.Articles.ToList();
			//var comments = _context.Comments.ToList();
			//var users = _context.Users.ToList();
			//var boards = _context.Boards.ToList();

			////var articles = await _articleService.GetAllEntityModelsAsync();
			////var comments = await _commentService.GetAllEntityModelsAsync();
			////var users = await _userService.GetAllEntityModelsAsync();
			////var boards = await _boardService.GetAllEntityModelsAsync();

			//var dictionary = new Dictionary<string, object>();
			//dictionary.Add("articles", articles);
			//dictionary.Add("comments", comments);
			//dictionary.Add("users", users);
			//dictionary.Add("boards", boards);

			//return Ok(dictionary);
		}

		[HttpGet]
		public IActionResult ThrowError()
		{
			throw new InvalidPostException(ModelState);
		}
	}
}
