using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Parameters;
using HackerNews.Helpers;
using HackerNews.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class ArticlesController : Controller
	{
		private readonly string articleEndpoint = "articles";
		private readonly ArticleApiConsumer _articleConsumer;

		public ArticlesController(ArticleApiConsumer articleConsumer)
		{
			_articleConsumer = articleConsumer;
		}

		public async Task<ViewResult> List(int pageNumber = 1, int pageSize = 10)
		{
			var pagingParams = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			var articleModels = await _articleConsumer.GetEndpointAsync(articleEndpoint, pagingParams);

			var viewModel = new ArticlesListViewModel { ArticleModels = articleModels };

			return View(viewModel);
		}

		public async Task<ViewResult> Details(int id)
		{
			var articleModel = await _articleConsumer.GetEndpointAsync(articleEndpoint, id);

			return View(new ArticlesDetailsViewModel { ArticleModel = articleModel });
		}

		public ViewResult Create()
		{
			return View(new PostArticleModel());
		}

		[HttpPost]
		public async Task<ActionResult> Post(PostArticleModel article)
		{
			GetArticleModel model = (GetArticleModel)await _articleConsumer.PostEndpointAsync(articleEndpoint, article);

			return RedirectToAction("Details", new { model.Id });
		}
	}
}
