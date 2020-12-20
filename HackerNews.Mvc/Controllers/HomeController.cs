using HackerNews.Application.Articles.Queries.GetArticlesBySearch;
using HackerNews.Application.Articles.Queries.GetArticlesWithPagination;
using HackerNews.Application.Boards.Queries.GetBoardsBySearch;
using HackerNews.Application.Boards.Queries.GetBoardsWithPagination;
using HackerNews.Application.Comments.Queries.GetCommentsBySearch;
using HackerNews.Application.Users.Queries.GetUsersBySearch;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Models;
using HackerNews.Mvc.ViewModels.Home;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Controllers
{
	public class HomeController : FrontendController
	{

		public async Task<IActionResult> Index(PagingParams pagingParams)
		{
			var articles = await Mediator.Send(new GetArticlesWithPaginationQuery(pagingParams));

			var viewModel = new HomeIndexViewModel { ArticlePage = new FrontendPage<GetArticleModel>(articles) };
			return View(viewModel);
		}

		public async Task<ActionResult<HomeSearchViewModel>> Search(string searchTerm)
		{
			var pagingParams = new PagingParams { PageNumber = 1, PageSize = 3 };

			var matchingBoards = await Mediator.Send(new GetBoardsBySearchQuery(searchTerm, pagingParams));

			var matchingUsers = await Mediator.Send(new GetUsersBySearchQuery(searchTerm, pagingParams));
			var matchingArticles = await Mediator.Send(new GetArticlesBySearchQuery(searchTerm, pagingParams));
			var matchingComments = await Mediator.Send(new GetCommentsBySearchQuery(searchTerm, pagingParams));

			var model = new HomeSearchViewModel
			{
				SearchTerm = searchTerm,
				ArticlePage = new FrontendPage<GetArticleModel>(matchingArticles),
				BoardPage = new FrontendPage<GetBoardModel>(matchingBoards),
				CommentPage = new FrontendPage<GetCommentModel>(matchingComments),
				UserPage = new FrontendPage<GetPublicUserModel>(matchingUsers)
			};
			return View(model);
		}

		public async Task<ViewResult> Boards(PagingParams pagingParams)
		{
			var boardModels = await Mediator.Send(new GetBoardsWithPaginationQuery(pagingParams));
			var viewModel = new HomeBoardsViewModel { BoardPage = new FrontendPage<GetBoardModel>(boardModels) };
			return View(viewModel);
		}

		public ActionResult Error()
		{
			var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
			var error = exceptionHandlerPathFeature?.Error;
			string exceptionMessage = "There was an internal error.";

			if (error is UnauthorizedException)
			{
				return RedirectToAction("Login", "Users");
			}
			else
			{
				exceptionMessage = error.Message;
			}

			var model = new ErrorViewModel
			{
				ExceptionMessage = exceptionMessage
			};
			return View(model);
		}
	}
}
