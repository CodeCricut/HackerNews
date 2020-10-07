using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Parameters;
using HackerNews.Helpers;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels;
using HackerNews.ViewModels.Articles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class ArticlesController : Controller
	{
		private static readonly string ARTICLE_ENDPOINT = "articles";
		private readonly IApiReader<GetArticleModel> _articleReader;
		private readonly IApiModifier<Article, PostArticleModel, GetArticleModel> _articleModifier;

		public ArticlesController(
			IApiReader<GetArticleModel> articleReader,
			IApiModifier<Article, PostArticleModel, GetArticleModel> articleModifier)
		{
			_articleReader = articleReader;
			_articleModifier = articleModifier;
		}

		public async Task<ViewResult> List(int pageNumber = 1, int pageSize = 10)
		{
			var pagingParams = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			var articleModels = await _articleReader.GetEndpointAsync(ARTICLE_ENDPOINT, pagingParams);
		
			var viewModel = new ArticleListViewModel { GetModels = articleModels };

			return View(viewModel);
		}

		public async Task<ViewResult> Details(int id)
		{
			var articleModel = await _articleReader.GetEndpointAsync(ARTICLE_ENDPOINT, id);

			return View(new ArticleDetailsViewModel { GetModel = articleModel });
		}

		public ViewResult Create()
		{
			var model = new ArticleCreateViewModel { PostModel = new PostArticleModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Post(PostArticleModel article)
		{
			GetArticleModel model = await _articleModifier.PostEndpointAsync(ARTICLE_ENDPOINT, article);

			return RedirectToAction("Details", new { model.Id });
		}
	}
}
