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
		private readonly IApiReader<GetArticleModel> _articleReader;

		public HomeController(IApiReader<GetArticleModel> articleReader)
		{
			_articleReader = articleReader;
		}

		public async Task<IActionResult> Index(PagingParams pagingParams)
		{
			var articles = await _articleReader.GetEndpointAsync("articles", pagingParams);
			var viewModel = new HomeIndexViewModel(articles);
			return View(viewModel);
		}


	}
}
