using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
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
		private readonly IApiModifier<Article, PostArticleModel, GetArticleModel> _articleModifier;
		private readonly IApiModifier<Comment, PostCommentModel, GetCommentModel> _commentModifier;
		private readonly IApiReader _apiReader;

		public ArticlesController(
			IApiModifier<Article, PostArticleModel, GetArticleModel> articleModifier,
			IApiModifier<Comment, PostCommentModel, GetCommentModel> commentModifier,
			IApiReader apiReader)
		{
			_articleModifier = articleModifier;
			_commentModifier = commentModifier;
			_apiReader = apiReader;
		}

		public async Task<ViewResult> Details(int id)
		{
			var articleModel = await _apiReader.GetEndpointAsync<GetArticleModel>(ARTICLE_ENDPOINT, id);
			// TODO: there is an endpoint for gettign many by IDs now
			var comments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", articleModel.CommentIds);
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", articleModel.BoardId);
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", articleModel.UserId);

			return View(new ArticleDetailsViewModel { GetModel = articleModel, Comments = comments, Board = board, User = user });
		}

		public ViewResult Create()
		{
			var model = new ArticleCreateViewModel { PostModel = new PostArticleModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Post(ArticleCreateViewModel viewModel)
		{
			var postModel = viewModel.PostModel;
			GetArticleModel model = await _articleModifier.PostEndpointAsync(ARTICLE_ENDPOINT, postModel);

			return RedirectToAction("Details", new { model.Id });
		}

		[HttpPost]
		public async Task<ActionResult> AddComment([Bind("GetModel, PostCommentModel")] ArticleDetailsViewModel viewModel)
		{
			var commentAdded = viewModel.PostCommentModel;
			commentAdded.BoardId = viewModel.GetModel.BoardId;
			commentAdded.ParentArticleId = viewModel.GetModel.Id;

			await _commentModifier.PostEndpointAsync("Comments", commentAdded);

			return RedirectToAction("Details", new { id = viewModel.GetModel.Id });
		}
	}
}
