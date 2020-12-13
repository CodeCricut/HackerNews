﻿using HackerNews.Application.Articles.Commands.AddArticle;
using HackerNews.Application.Articles.Commands.AddImage;
using HackerNews.Application.Articles.Commands.DeleteArticle;
using HackerNews.Application.Articles.Commands.VoteArticle;
using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Articles.Queries.GetArticlesBySearch;
using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Comments.Commands.AddComment;
using HackerNews.Application.Comments.Queries.GetCommentsByIds;
using HackerNews.Application.Users.Commands.SaveArticleToUser;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Models;
using HackerNews.Mvc.ViewModels.Articles;
using HackerNews.Web.Pipeline.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Controllers
{
	public class ArticlesController : FrontendController
	{
		[JwtAuthorize]
		public ViewResult Create(int boardId)
		{
			var model = new ArticleCreateViewModel { Article = new PostArticleModel() { BoardId = boardId } };
			return View(model);
		}

		[HttpPost]
		[JwtAuthorize]
		public async Task<ActionResult> Post(ArticleCreateViewModel viewModel)
		{
			GetArticleModel model = await Mediator.Send(new AddArticleCommand(viewModel.Article));

			return RedirectToAction("Details", new { model.Id });
		}

		public async Task<ViewResult> Details(int id, PagingParams pagingParams)
		{
			var articleModel = await Mediator.Send(new GetArticleQuery(id));
			var articleComments = await Mediator.Send(new GetCommentsByIdsQuery(articleModel.CommentIds, pagingParams));
			var board = await Mediator.Send(new GetBoardQuery(articleModel.BoardId));
			var user = await Mediator.Send(new GetPublicUserQuery(articleModel.UserId));

			var privateUser = await TryGetPrivateUser();

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
				CommentPage = new FrontendPage<GetCommentModel>(articleComments),
				LoggedIn = loggedIn,
				User = user,
				UserSavedArticle = savedArticle,
				UserWroteArticle = wroteArticle
			};

			return View(viewModel);
		}

		[HttpPost]
		[JwtAuthorize]
		public async Task<ActionResult> AddComment(ArticleDetailsViewModel viewModel)
		{
			var comment = viewModel.PostCommentModel;
			comment.ParentArticleId = viewModel.Article.Id;

			await Mediator.Send(new AddCommentCommand(comment));

			return RedirectToAction("Details", new { id = viewModel.Article.Id });
		}

		[JwtAuthorize]
		public async Task<ActionResult> Vote(int id, bool upvote)
		{
			await Mediator.Send(new VoteArticleCommand(id, upvote));
			return RedirectToAction("Details", new { id });
		}

		[JwtAuthorize]
		public async Task<ActionResult> SaveArticle(int id)
		{
			await Mediator.Send(new SaveArticleToUserCommand(id));
			return RedirectToAction("Details", new { id });
		}

		public async Task<ActionResult<ArticleSearchViewModel>> Search(string searchTerm, PagingParams pagingParams)
		{
			var articles = await Mediator.Send(new GetArticlesBySearchQuery(searchTerm, pagingParams));
			var model = new ArticleSearchViewModel { SearchTerm = searchTerm, ArticlePage = new FrontendPage<GetArticleModel>(articles) };
			return View(model);
		}

		[JwtAuthorize]
		public async Task<IActionResult> Delete(int id)
		{
			await Mediator.Send(new DeleteArticleCommand(id));
			return RedirectToAction("Details", new { id });
		}

		private async Task<GetPrivateUserModel> TryGetPrivateUser()
		{
			try
			{
				return await Mediator.Send(new GetAuthenticatedUserQuery());
			}
			catch (NotFoundException)
			{
				return null;
			}
		}

		[JwtAuthorize]
		public ActionResult<ArticleAddImageViewModel> AddImage(int id)
		{
			var model = new ArticleAddImageViewModel
			{
				ArticleId = id,
				PostImageModel = new PostImageModel()
			};
			return View(model);
		}

		[HttpPost]
		[JwtAuthorize]
		public async Task<ActionResult> PostImage(ArticleAddImageViewModel postModel)
		{
			var file = Request.Form.Files.FirstOrDefault();
			if (file == null) return RedirectToAction("Details", new { id = postModel.ArticleId });

			// Copy the image data to the image object
			PostImageModel img = new PostImageModel();
			using MemoryStream ms = new MemoryStream();
			file.CopyTo(ms);
			img.ImageData = ms.ToArray();
			ms.Close();

			// Add other image fields
			img.ImageTitle = postModel.PostImageModel.ImageTitle;
			img.ImageDescription = postModel.PostImageModel.ImageDescription;


			var updateArticle = await Mediator.Send(new AddArticleImageCommand(img, postModel.ArticleId));
			return RedirectToAction("Details", new { id = updateArticle.Id });
		} 
	}
}
