using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanEntityArchitecture.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Home;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;

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
			var articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", pagingParams);
			var viewModel = new HomeIndexViewModel(articles);
			return View(viewModel);
		}






		public async Task<ActionResult<HomeSearchViewModel>> Search(string searchTerm, PagingParams pagingParams)
		{
			var matchingBoards = await _apiReader.GetEndpointWithQueryAsync<GetBoardModel>("boards", searchTerm, pagingParams);
			var matchingUsers = await _apiReader.GetEndpointWithQueryAsync<GetPublicUserModel>("users", searchTerm, pagingParams);
			var matchingArticles = await _apiReader.GetEndpointWithQueryAsync<GetArticleModel>("articles", searchTerm, pagingParams);
			var matchingComments = await _apiReader.GetEndpointWithQueryAsync<GetCommentModel>("comments", searchTerm, pagingParams);

			var model = new HomeSearchViewModel
			{
				Articles = matchingArticles,
				Boards = matchingBoards,
				Comments = matchingComments,
				Users = matchingUsers
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Search(NavbarViewModel viewModel)
		{

			return RedirectToAction("Search", new { searchTerm = viewModel.SearchTerm, pageNumber = 1, pageSize=3 });
		}


	}
}
