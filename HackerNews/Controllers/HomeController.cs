using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanEntityArchitecture.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Home;
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


	}
}
