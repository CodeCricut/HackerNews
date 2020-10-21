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
		private readonly IApiBoardModeratorAdder _moderatorAdder;
		private readonly IApiReader _apiReader;

		public BoardsController(
			IApiModifier<Board, PostBoardModel, GetBoardModel> boardModifier,
			IApiBoardModeratorAdder moderatorAdder,
			IApiReader apiReader)
		{
			_boardModifier = boardModifier;
			_moderatorAdder = moderatorAdder;
			_apiReader = apiReader;
		}

		public ViewResult Create()
		{
			var model = new BoardCreateModel { PostModel = new PostBoardModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Post(BoardCreateModel boardCreateModel)
		{
			GetBoardModel model = await _boardModifier.PostEndpointAsync(BOARD_ENDPOINT, boardCreateModel.PostModel);

			return RedirectToAction("Details", new { id =  model.Id });
		}

		public async Task<ActionResult> Details(int id, PagingParams pagingParams)
		{
			GetBoardModel getBoardModel = await _apiReader.GetEndpointAsync<GetBoardModel>(BOARD_ENDPOINT, id);
			PagedListResponse<GetArticleModel> articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", getBoardModel.ArticleIds, pagingParams);

			var model = new BoardDetailsViewModel(articles) { GetModel = getBoardModel };
			return View(model);
		}

		public async Task<ViewResult> List(int pageNumber = 1, int pageSize = 10)
		{
			var pagingParams = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			var boardModels = await _apiReader.GetEndpointAsync<GetBoardModel>(BOARD_ENDPOINT, pagingParams);

			var viewModel = new BoardListViewModel { GetModels = boardModels.Items };

			return View(viewModel);
		}

		public async Task<ActionResult<BoardAdminViewModel>> Admin(int id)
		{
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>(BOARD_ENDPOINT, id);
			// TODO: you shoul dnot have to pass board name to method; should be setup in each implementation
			var moderators = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", board.ModeratorIds);
			var subscribers = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", board.SubscriberIds);

			return View(new BoardAdminViewModel { Board = board, Moderators = moderators, Subscribers = subscribers });
		}

		public async Task<ActionResult> AddModerator([Bind(include: new string[] { "Board", "ModeratorAddedId" })] BoardAdminViewModel model)
		{
			var updatedBoard = await _moderatorAdder.AddModerator(model.Board.Id, model.ModeratorAddedId);

			return RedirectToAction("Admin", new { id = updatedBoard.Id });
		}

		public async Task<ActionResult<BoardModeratorsListViewModel>> Moderators(int id)
		{
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>(BOARD_ENDPOINT, id);

			// TODO: there should be an API endpoint to get a list of things by ID
			var moderators = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", board.ModeratorIds);

			var model = new BoardModeratorsListViewModel { Moderators = moderators };
			return View(model);
		}

		public async Task<ActionResult<BoardArticlesListViewModel>> Articles(int id)
		{
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>(BOARD_ENDPOINT, id);
			var artices = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", board.ArticleIds);

			var model = new BoardArticlesListViewModel { Articles = artices };
			return View(model);
		}
	}
}
