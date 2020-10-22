using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class HomeController : Controller
	{
		private readonly IApiReader _apiReader;

		public HomeController(IApiReader apiReader)
		{
			_apiReader = apiReader;
		}

		public async Task<IActionResult> Index(PagingParams pagingParams)
		{
			var articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", pagingParams); ;

			var viewModel = new HomeIndexViewModel { ArticlePage = new Page<GetArticleModel>(articles) };
			return View(viewModel);
		}

		public async Task<ActionResult<HomeSearchViewModel>> Search(string searchTerm)
		{
			var pagingParams = new PagingParams { PageNumber = 1, PageSize = 3 };

			var matchingBoards = await _apiReader.GetEndpointWithQueryAsync<GetBoardModel>("boards", searchTerm, pagingParams);
			var matchingUsers = await _apiReader.GetEndpointWithQueryAsync<GetPublicUserModel>("users", searchTerm, pagingParams);
			var matchingArticles = await _apiReader.GetEndpointWithQueryAsync<GetArticleModel>("articles", searchTerm, pagingParams);
			var matchingComments = await _apiReader.GetEndpointWithQueryAsync<GetCommentModel>("comments", searchTerm, pagingParams);

			var model = new HomeSearchViewModel
			{
				SearchTerm = searchTerm,
				ArticlePage = new Page<GetArticleModel>(matchingArticles),
				BoardPage = new Page<GetBoardModel>(matchingBoards),
				CommentPage = new Page<GetCommentModel>(matchingComments),
				UserPage = new Page<GetPublicUserModel>(matchingUsers)
			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Search(HomeIndexViewModel viewModel)
		{
			return RedirectToAction("Search", new { searchTerm = viewModel.SearchTerm });
		}


		public async Task<ViewResult> Boards(PagingParams pagingParams)
		{
			var boardModels = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", pagingParams);

			var viewModel = new HomeBoardsViewModel { BoardPage = new Page<GetBoardModel>(boardModels) };
			return View(viewModel);
		}

	}
}
