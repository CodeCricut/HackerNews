﻿using CleanEntityArchitecture.Domain;
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

		public ViewResult Create(int boardId)
		{
			var model = new ArticleCreateViewModel { Article = new PostArticleModel() { BoardId = boardId } };
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Post(ArticleCreateViewModel viewModel)
		{
			GetArticleModel model = await _articleModifier.PostEndpointAsync(ARTICLE_ENDPOINT, viewModel.Article);

			return RedirectToAction("Details", new { model.Id });
		}

		public async Task<ViewResult> Details(int id, PagingParams pagingParams)
		{
			var articleModel = await _apiReader.GetEndpointAsync<GetArticleModel>(ARTICLE_ENDPOINT, id);
			// TODO: there is an endpoint for gettign many by IDs now
			var pagedCommentResponse = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", articleModel.CommentIds, pagingParams);
			var commentPage = new Page<GetCommentModel>(pagedCommentResponse);

			var board = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", articleModel.BoardId);
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", articleModel.UserId);

			var privateUser = await _apiReader.GetEndpointAsync<GetPrivateUserModel>("users/me");
			var loggedIn = privateUser != null && privateUser.Id != 0;

			var savedArticle = privateUser.SavedArticles.Contains(id);

			var viewModel = new ArticleDetailsViewModel
			{
				Article = articleModel,
				Board = board,
				CommentPage = commentPage,
				LoggedIn = loggedIn,
				User = user,
				UserSavedArticle = savedArticle
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<ActionResult> AddComment([Bind("GetModel, PostCommentModel")] ArticleDetailsViewModel viewModel)
		{
			var commentAdded = viewModel.PostCommentModel;
			commentAdded.ParentArticleId = viewModel.Article.Id;

			await _commentModifier.PostEndpointAsync("Comments", commentAdded);

			return RedirectToAction("Details", new { id = viewModel.Article.Id });
		}

		public async Task<ActionResult> Vote(int id, bool upvote)
		{
			await _articleVoter.VoteEntityAsync(id, upvote);
			return RedirectToAction("Details", new { id });
		}

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
	}
}
