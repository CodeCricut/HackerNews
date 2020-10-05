//using HackerNews.Api.Helpers.EntityHelpers;
//using HackerNews.Api.Helpers.EntityServices.Base;
//using HackerNews.Api.Helpers.EntityServices.Base.ArticleServices;
//using HackerNews.Domain;
//using HackerNews.Domain.Errors;
//using HackerNews.EF;
//using HackerNews.EF.Repositories;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HackerNews.Api.Controllers
//{
//	[Route("api/[controller]")]
//	public class DeveloperController : ControllerBase
//	{
//		private readonly IEntityRepository<Article> _articleRepository;
//		private readonly IEntityRepository<Comment> _commentRepository;
//		private readonly IEntityRepository<User> _userRepository;
//		private readonly IEntityRepository<Board> _boardRepository;
//		private readonly VoteArticleService _articleService;
//		private readonly CommentService _commentService;
//		private readonly BoardService _boardService;
//		private readonly UserService _userService;
//		private readonly HackerNewsContext _context;

//		public DeveloperController(
//			IEntityRepository<Article> articleRepository, 
//			IEntityRepository<Comment> commentRepository, 
//			IEntityRepository<User> userRepository,		
//			IEntityRepository<Board> boardRepository,
//			VoteArticleService articleService,
//			CommentService commentService,
//			BoardService boardService,
//			UserService userService,
//			HackerNewsContext context)
//		{
//			_articleRepository = articleRepository;
//			_commentRepository = commentRepository;
//			_userRepository = userRepository;
//			_boardRepository = boardRepository;
//			_articleService = articleService;
//			_commentService = commentService;
//			_boardService = boardService;
//			_userService = userService;
//			_context = context;
//		}

//		[HttpOptions("delete-all")]
//		public async Task<IActionResult> DeleteAllDataAsync()
//		{
//			try
//			{

//				var articles = _context.Articles.ToList();
//				var comments = _context.Comments.ToList();
//				var users = _context.Users.ToList();
//				var boards = _context.Boards.ToList();

//				_context.RemoveRange(articles);
//				_context.RemoveRange(comments);
//				_context.RemoveRange(users);
//				_context.RemoveRange(boards);

//				await _context.SaveChangesAsync();

//				return Ok();
//			}
//			catch
//			{
//				return StatusCode(StatusCodes.Status500InternalServerError);
//			}

//		}

//		[HttpOptions("get-all")]
//		public async Task<IActionResult> GetAll()
//		{
//			var articles = _context.Articles.ToList();
//			var comments = _context.Comments.ToList();
//			var users = _context.Users.ToList();
//			var boards = _context.Boards.ToList();

//			//var articles = await _articleService.GetAllEntityModelsAsync();
//			//var comments = await _commentService.GetAllEntityModelsAsync();
//			//var users = await _userService.GetAllEntityModelsAsync();
//			//var boards = await _boardService.GetAllEntityModelsAsync();

//			var dictionary = new Dictionary<string, object>();
//			dictionary.Add("articles", articles);
//			dictionary.Add("comments", comments);
//			dictionary.Add("users", users);
//			dictionary.Add("boards", boards);

//			return Ok(dictionary);
//		}

//		[HttpGet]
//		public IActionResult ThrowError()
//		{
//			throw new InvalidPostException(ModelState);
//		}
//	}
//}
