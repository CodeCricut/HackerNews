using CleanEntityArchitecture.Domain;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
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
		private readonly IApiReader<GetCommentModel> _commentReader;

		public ArticlesController(
			IApiReader<GetArticleModel> articleReader,
			IApiModifier<Article, PostArticleModel, GetArticleModel> articleModifier,
			IApiReader<GetCommentModel> commentReader)
		{
			_articleReader = articleReader;
			_articleModifier = articleModifier;
			_commentReader = commentReader;
		}

		public async Task<ViewResult> Details(int id)
		{
			var articleModel = await _articleReader.GetEndpointAsync(ARTICLE_ENDPOINT, id);
			var comments = await TaskHelper.RunConcurrentTasksAsync(articleModel.CommentIds, commentId => _commentReader.GetEndpointAsync("Comments", commentId));

			return View(new ArticleDetailsViewModel { GetModel = articleModel, Comments = comments });
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
