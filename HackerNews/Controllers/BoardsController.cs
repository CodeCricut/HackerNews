using CleanEntityArchitecture.Domain;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels;
using HackerNews.ViewModels.Boards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class BoardsController : Controller
	{
		private static readonly string BOARD_ENDPOINT = "boards";
		private readonly IApiModifier<Board, PostBoardModel, GetBoardModel> _boardModifier;
		private readonly IApiBoardModeratorAdder _moderatorAdder;
		private readonly IApiReader _apiReader;
		private readonly IApiBoardSubscriber _boardSubscriber;

		public BoardsController(
			IApiModifier<Board, PostBoardModel, GetBoardModel> boardModifier,
			IApiBoardModeratorAdder moderatorAdder,
			IApiReader apiReader,
			IApiBoardSubscriber boardSubscriber)
		{
			_boardModifier = boardModifier;
			_moderatorAdder = moderatorAdder;
			_apiReader = apiReader;
			_boardSubscriber = boardSubscriber;
		}

		[Authorize]
		public ViewResult Create()
		{
			var model = new BoardCreateModel { Board = new PostBoardModel() };
			return View(model);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> Post(BoardCreateModel boardCreateModel)
		{
			GetBoardModel model = await _boardModifier.PostEndpointAsync(BOARD_ENDPOINT, boardCreateModel.Board);

			return RedirectToAction("Details", new { id =  model.Id });
		}

		public async Task<ActionResult> Details(int id, PagingParams pagingParams)
		{
			GetBoardModel getBoardModel = await _apiReader.GetEndpointAsync<GetBoardModel>(BOARD_ENDPOINT, id);

			PagedListResponse<GetArticleModel> articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", getBoardModel.ArticleIds, pagingParams);

			var moderatorPagingParams = new PagingParams { PageNumber = 1, PageSize = 5 };
			var moderators = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", getBoardModel.ModeratorIds, moderatorPagingParams);

			var model = new BoardDetailsViewModel
			{
				Board = getBoardModel,
				ArticlePage = new Page<GetArticleModel>(articles),
				Moderators = new Page<GetPublicUserModel>(moderators)
			};

			return View(model);
		}

		[Authorize]
		public async Task<ActionResult<BoardAdminViewModel>> Admin(int id)
		{
			// TODO: ensure user is moderator

			var board = await _apiReader.GetEndpointAsync<GetBoardModel>(BOARD_ENDPOINT, id);

			// TODO: you shoul dnot have to pass board name to method; should be setup in each implementation
			var moderatorPagingParams = new PagingParams { PageNumber = 1, PageSize = 5 };
			var moderators = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", board.ModeratorIds, moderatorPagingParams);

			var viewModel = new BoardAdminViewModel
			{
				Board = board,
				ModeratorPage = new Page<GetPublicUserModel>(moderators)
			};

			return View(viewModel);
		}

		[Authorize]
		public async Task<ActionResult> AddModerator([Bind(include: new string[] { "Board", "ModeratorAddedId" })] BoardAdminViewModel model)
		{
			// TODO: ensure user is moderator

			var updatedBoard = await _moderatorAdder.AddModerator(model.Board.Id, model.ModeratorAddedId);

			return RedirectToAction("Admin", new { id = updatedBoard.Id });
		}

		public async Task<ActionResult<BoardModeratorsListViewModel>> Moderators(int id)
		{
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>(BOARD_ENDPOINT, id);

			// TODO: there should be an API endpoint to get a list of things by ID
			var moderatorPagingParams = new PagingParams { PageNumber = 1, PageSize = 5 };
			var moderators = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", board.ModeratorIds, moderatorPagingParams);

			var model = new BoardModeratorsListViewModel {  Board = board, ModeratorPage = new Page<GetPublicUserModel>(moderators) };
			return View(model);
		}

		[Authorize]
		public async Task<ActionResult> Subscribe(int boardId)
		{
			var updatedBoard = await _boardSubscriber.AddSubscriber(boardId);

			return RedirectToAction("Details", new { id = updatedBoard.Id });
		}

		public async Task<ActionResult<BoardSearchViewModel>> Search(string searchTerm, PagingParams pagingParams)
		{
			var boards = await _apiReader.GetEndpointWithQueryAsync<GetBoardModel>(BOARD_ENDPOINT, searchTerm, pagingParams);

			var model = new BoardSearchViewModel { BoardPage = new Page<GetBoardModel>(boards), SearchTerm = searchTerm };
			return View(model);
		}
	}
}
