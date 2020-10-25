using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels;
using HackerNews.ViewModels.Articles;
using Microsoft.AspNetCore.Authorization;
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
		private readonly IApiVoter<Article> _articleVoter;
		private readonly IApiUserSaver<Article> _articleSaver;

		public ArticlesController(
			IApiModifier<Article, PostArticleModel, GetArticleModel> articleModifier,
			IApiModifier<Comment, PostCommentModel, GetCommentModel> commentModifier,
			IApiReader apiReader,
			IApiVoter<Article> articleVoter,
			IApiUserSaver<Article> articleSaver)
		{
			_articleModifier = articleModifier;
			_commentModifier = commentModifier;
			_apiReader = apiReader;
			_articleVoter = articleVoter;
			_articleSaver = articleSaver;
		}

		[Authorize]
		public ViewResult Create(int boardId)
		{
			var model = new ArticleCreateViewModel { Article = new PostArticleModel() { BoardId = boardId } };
			return View(model);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> Post(ArticleCreateViewModel viewModel)
		{
			GetArticleModel model = await _articleModifier.PostEndpointAsync(ARTICLE_ENDPOINT, viewModel.Article);

			return RedirectToAction("Details", new { model.Id });
		}

		public async Task<ViewResult> Details(int id, PagingParams pagingParams)
		{
			var articleModel = await _apiReader.GetEndpointAsync<GetArticleModel>(ARTICLE_ENDPOINT, id, includeDeleted: true);
			// TODO: there is an endpoint for gettign many by IDs now
			var pagedCommentResponse = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", articleModel.CommentIds, pagingParams);
			var commentPage = new Page<GetCommentModel>(pagedCommentResponse);

			var board = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", articleModel.BoardId);
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", articleModel.UserId);

			var privateUser = await _apiReader.GetEndpointAsync<GetPrivateUserModel>("users/me");
			var loggedIn = privateUser != null && privateUser.Id != 0;

			var savedArticle = loggedIn
				? privateUser.SavedArticles.Contains(id)
				: false;

			var wroteArticle = loggedIn
				? privateUser.ArticleIds.Contains(id)
				: false;

			var viewModel = new ArticleDetailsViewModel
			{
				Article = articleModel,
				Board = board,
				CommentPage = commentPage,
				LoggedIn = loggedIn,
				User = user,
				UserSavedArticle = savedArticle,
				UserWroteArticle = wroteArticle
			};

			return View(viewModel);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> AddComment([Bind("GetModel, PostCommentModel")] ArticleDetailsViewModel viewModel)
		{
			var commentAdded = viewModel.PostCommentModel;
			commentAdded.ParentArticleId = viewModel.Article.Id;

			await _commentModifier.PostEndpointAsync("Comments", commentAdded);

			return RedirectToAction("Details", new { id = viewModel.Article.Id });
		}

		[Authorize]
		public async Task<ActionResult> Vote(int id, bool upvote)
		{
			await _articleVoter.VoteEntityAsync(id, upvote);
			return RedirectToAction("Details", new { id });
		}

		[Authorize]
		public async Task<ActionResult> SaveArticle(int id)
		{
			await _articleSaver.SaveEntityToUserAsync(id);

			return RedirectToAction("Details", new { id });
		}

		public async Task<ActionResult<ArticleSearchViewModel>> Search(string searchTerm, PagingParams pagingParams)
		{

			var articles = await _apiReader.GetEndpointWithQueryAsync<GetArticleModel>(ARTICLE_ENDPOINT, searchTerm, pagingParams);

			var model = new ArticleSearchViewModel { SearchTerm = searchTerm, ArticlePage = new Page<GetArticleModel>(articles) };
			return View(model);
		}

		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			await _articleModifier.DeleteEndpointAsync(ARTICLE_ENDPOINT, id);

			return RedirectToAction("Details", new { id });
		}
	}
}
