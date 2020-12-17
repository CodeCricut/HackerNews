using HackerNews.Application.Articles.Queries.GetArticlesByIds;
using HackerNews.Application.Boards.Commands.AddBoard;
using HackerNews.Application.Boards.Commands.AddImage;
using HackerNews.Application.Boards.Commands.AddModerator;
using HackerNews.Application.Boards.Commands.AddSubscriber;
using HackerNews.Application.Boards.Commands.DeleteBoard;
using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Boards.Queries.GetBoardsBySearch;
using HackerNews.Application.Images.Queries.GetImageById;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUsersByIds;
using HackerNews.Application.Users.Queries.GetUserByUsername;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Models;
using HackerNews.Mvc.Services.Interfaces;
using HackerNews.Mvc.ViewModels.Boards;
using HackerNews.Web.Pipeline.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Controllers
{
	public class BoardsController : FrontendController
	{
		private readonly IImageFileReader _imageFileReader;
		private readonly IImageDataHelper _imageDataHelper;

		public BoardsController(IImageFileReader imageFileReader, IImageDataHelper imageDataHelper)
		{
			_imageFileReader = imageFileReader;
			_imageDataHelper = imageDataHelper;
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
			GetBoardModel addedBoard = await Mediator.Send(new AddBoardCommand(boardCreateModel.Board));

			var file = Request.Form.Files.FirstOrDefault();
			if (file != null)
			{
				PostImageModel imageModel = _imageFileReader.ConvertImageFileToImageModel(file);
				await Mediator.Send(new AddBoardImageCommand(imageModel, addedBoard.Id));
			}

			return RedirectToAction("Details", new { id = addedBoard.Id });
		}

		public async Task<ActionResult> Details(int id, PagingParams pagingParams)
		{
			GetBoardModel getBoardModel = await Mediator.Send(new GetBoardQuery(id));
			var boardArticles = await Mediator.Send(new GetArticlesByIdsQuery(getBoardModel.ArticleIds, pagingParams));

			var moderatorPagingParams = new PagingParams { PageNumber = 1, PageSize = 5 };
			var boardModerators = await Mediator.Send(new GetPublicUsersByIdsQuery(getBoardModel.ModeratorIds, moderatorPagingParams));

			string imageDataURL = "";
			if (getBoardModel.BoardImageId > 0)
			{
				GetImageModel img = await Mediator.Send(new GetImageByIdQuery(getBoardModel.BoardImageId));
				imageDataURL = _imageDataHelper.ConvertImageDataToDataUrl(img.ImageData, img.ContentType);
			}


			var model = new BoardDetailsViewModel
			{
				Board = getBoardModel,
				ArticlePage = new FrontendPage<GetArticleModel>(boardArticles),
				Moderators = new FrontendPage<GetPublicUserModel>(boardModerators),
				ImageDataUrl = imageDataURL
			};

			return View(model);
		}

		[Authorize]
		public async Task<ActionResult<BoardAdminViewModel>> Admin(int id)
		{
			var user = await Mediator.Send(new GetAuthenticatedUserQuery());

			if (!user.BoardsModerating.Contains(id)) return RedirectToAction("Details", new { id });

			var board = await Mediator.Send(new GetBoardQuery(id));

			var moderatorPagingParams = new PagingParams { PageNumber = 1, PageSize = 5 };
			var moderators = await Mediator.Send(new GetPublicUsersByIdsQuery(board.ModeratorIds, moderatorPagingParams));

			var viewModel = new BoardAdminViewModel
			{
				Board = board,
				ModeratorPage = new FrontendPage<GetPublicUserModel>(moderators),
				UserCreatedBoard = board.CreatorId == user.Id
			};

			return View(viewModel);
		}

		[Authorize]
		public async Task<ActionResult> AddModerator(BoardAdminViewModel model)
		{
			try
			{
				var toAddModerator = await Mediator.Send(new GetUserByUsernameQuery(model.ModeratorAddedUsername));

				var updatedBoard = await Mediator.Send(new AddModeratorCommand(model.Board.Id, toAddModerator.Id));

				return RedirectToAction("Admin", new { id = updatedBoard.Id });
			}
			catch (NotFoundException e)
			{
				return RedirectToAction("Admin", new { id = model.Board.Id });
			}

		}

		public async Task<ActionResult<BoardModeratorsViewModel>> Moderators(int id)
		{
			var board = await Mediator.Send(new GetBoardQuery(id));

			var moderatorPagingParams = new PagingParams { PageNumber = 1, PageSize = 5 };
			var moderators = await Mediator.Send(new GetPublicUsersByIdsQuery(board.ModeratorIds, moderatorPagingParams));

			var model = new BoardModeratorsViewModel { Board = board, ModeratorPage = new FrontendPage<GetPublicUserModel>(moderators) };
			return View(model);
		}

		[Authorize]
		public async Task<ActionResult> Subscribe(int boardId)
		{
			var updatedBoard = await Mediator.Send(new AddSubscriberCommand(boardId));
			return RedirectToAction("Details", new { id = updatedBoard.Id });
		}

		public async Task<ActionResult<BoardSearchViewModel>> Search(string searchTerm, PagingParams pagingParams)
		{
			var boards = await Mediator.Send(new GetBoardsBySearchQuery(searchTerm, pagingParams));
			var model = new BoardSearchViewModel { BoardPage = new FrontendPage<GetBoardModel>(boards), SearchTerm = searchTerm };
			return View(model);
		}

		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			await Mediator.Send(new DeleteBoardCommand(id));
			return RedirectToAction("Details", new { id });
		}
	}
}
