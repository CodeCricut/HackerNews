using CleanEntityArchitecture.Domain;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels;
using HackerNews.ViewModels.Boards;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class BoardsController : Controller
	{
		private static readonly string BOARD_ENDPOINT = "boards";
		private readonly IApiModifier<Board, PostBoardModel, GetBoardModel> _boardModifier;
		private readonly IApiReader<GetBoardModel> _boardReader;
		private readonly IApiReader<GetPrivateUserModel> _privateUserReader;
		private readonly IApiBoardModeratorAdder _moderatorAdder;
		private readonly IApiReader<GetPublicUserModel> _publicUserReader;
		private readonly IApiReader<GetArticleModel> _articleReader;

		public BoardsController(
			IApiModifier<Board, PostBoardModel, GetBoardModel> boardModifier,
			IApiReader<GetBoardModel> boardReader,
			IApiReader<GetPrivateUserModel> privateUserReader,
			IApiBoardModeratorAdder moderatorAdder,
			IApiReader<GetPublicUserModel> publicUserReader,
			IApiReader<GetArticleModel> articleReader)
		{
			_boardModifier = boardModifier;
			_boardReader = boardReader;
			_privateUserReader = privateUserReader;
			_moderatorAdder = moderatorAdder;
			_publicUserReader = publicUserReader;
			_articleReader = articleReader;
		}

		public ViewResult Create()
		{
			var model = new BoardCreateModel { PostModel = new PostBoardModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Post(PostBoardModel board)
		{
			GetBoardModel model = await _boardModifier.PostEndpointAsync(BOARD_ENDPOINT, board);

			return RedirectToAction("Details", model.Id);
		}

		public async Task<ActionResult> Details(int id)
		{
			GetBoardModel getBoardModel = await _boardReader.GetEndpointAsync(BOARD_ENDPOINT, id);

			var model = new BoardDetailsViewModel { GetModel = getBoardModel };
			return View(model);
		}

		public async Task<ViewResult> List(int pageNumber = 1, int pageSize = 10)
		{
			var pagingParams = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			var boardModels = await _boardReader.GetEndpointAsync(BOARD_ENDPOINT, pagingParams);

			var viewModel = new BoardListViewModel { GetModels = boardModels };

			return View(viewModel);
		}

		public async Task<ActionResult<BoardAdminViewModel>> Admin(int id)
		{
			var board = await _boardReader.GetEndpointAsync(BOARD_ENDPOINT, id);
			// TODO: you shoul dnot have to pass board name to method; should be setup in each implementation
			var moderators = await TaskHelper.RunConcurrentTasksAsync(board.ModeratorIds, modId => _publicUserReader.GetEndpointAsync("Users", modId));
			var subscribers = await TaskHelper.RunConcurrentTasksAsync(board.SubscriberIds, subId => _publicUserReader.GetEndpointAsync("Subscribers", subId));

			return View(new BoardAdminViewModel { Board = board, Moderators = moderators, Subscribers = subscribers });
		}

		public async Task<ActionResult> AddModerator([Bind(include: new string[] { "Board", "ModeratorAddedId" })] BoardAdminViewModel model)
		{
			var updatedBoard = await _moderatorAdder.AddModerator(model.Board.Id, model.ModeratorAddedId);

			return RedirectToAction("Admin", new { id = updatedBoard.Id });
		}

		public async Task<ActionResult<BoardModeratorsListViewModel>> Moderators(int id)
		{
			var board = await _boardReader.GetEndpointAsync(BOARD_ENDPOINT, id);

			// TODO: there should be an API endpoint to get a list of things by ID
			var moderators = await TaskHelper.RunConcurrentTasksAsync(board.ModeratorIds, modId => _publicUserReader.GetEndpointAsync("Users", modId));

			var model = new BoardModeratorsListViewModel { Moderators = moderators };
			return View(model);
		}

		public async Task<ActionResult<BoardArticlesListViewModel>> Articles(int id)
		{
			var board = await _boardReader.GetEndpointAsync(BOARD_ENDPOINT, id);

			var artices = await TaskHelper.RunConcurrentTasksAsync(board.ArticleIds, articleId => _articleReader.GetEndpointAsync("Articles", articleId));

			var model = new BoardArticlesListViewModel { Articles = artices };
			return View(model);
		}
	}
}
