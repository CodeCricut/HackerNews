using HackerNews.Application.Articles.Commands.AddArticle;
using HackerNews.Application.Articles.Commands.AddImage;
using HackerNews.Application.Articles.Commands.DeleteArticle;
using HackerNews.Application.Articles.Commands.UpdateArticle;
using HackerNews.Application.Articles.Commands.VoteArticle;
using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Articles.Queries.GetArticlesBySearch;
using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Comments.Commands.AddComment;
using HackerNews.Application.Comments.Queries.GetCommentsByIds;
using HackerNews.Application.Common.Helpers;
using HackerNews.Application.Images.Queries.GetImageById;
using HackerNews.Application.Users.Commands.SaveArticleToUser;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Models;
using HackerNews.Mvc.Services.Interfaces;
using HackerNews.Mvc.ViewModels.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Controllers
{
	public class ArticlesController : FrontendController
	{
		private readonly IImageFileReader _imageFileReader;
		private readonly IImageDataHelper _imageDataHelper;

		public ArticlesController(IImageFileReader imageFileReader, IImageDataHelper imageDataHelper)
		{
			_imageFileReader = imageFileReader;
			_imageDataHelper = imageDataHelper;
		}

		[Authorize]
		public ViewResult Create(int boardId)
		{
			var model = new ArticleCreateViewModel { Article = new PostArticleModel() { BoardId = boardId } };
			return View(model);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> Create(ArticleCreateViewModel viewModel)
		{
			if (!ModelState.IsValid) return View(viewModel);

			// Create the article
			GetArticleModel model = await Mediator.Send(new AddArticleCommand(viewModel.Article));

			// If image attatched
			var file = Request.Form.Files.FirstOrDefault();
			if (file != null)
			{
				PostImageModel imageModel = _imageFileReader.ConvertImageFileToImageModel(file);

				// Add the image to the article
				await Mediator.Send(new AddArticleImageCommand(imageModel, model.Id));
			}

			return RedirectToAction("Details", new { model.Id });
		}

		[Authorize]
		public async Task<ViewResult> Edit(int id)
		{
			var article = await Mediator.Send(new GetArticleQuery(id));

			var model = new ArticleEditViewModel
			{
				ArticleId = id,
				PostArticleModel = new PostArticleModel
				{
					BoardId = article.BoardId,
					Text = article.Text,
					Title = article.Title,
					Type = article.Type.ToString()
				}
			};
			return View(model);
		}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult> Edit(ArticleEditViewModel editModel)
		{
			if (!ModelState.IsValid) return View(editModel);

			GetArticleModel updatedModel = await Mediator.Send(new UpdateArticleCommand(editModel.ArticleId, editModel.PostArticleModel));
			return RedirectToAction("Details", new { id = updatedModel.Id });
		}



		public async Task<ViewResult> Details(int id, PagingParams pagingParams)
		{
			var articleModel = await Mediator.Send(new GetArticleQuery(id));

			var getArticleCommentsQuery = new GetCommentsByIdsQuery(articleModel.CommentIds, pagingParams);
			var articleComments = await getArticleCommentsQuery.DefaultIfExceptionAsync(Mediator);

			var getBoardQuery = new GetBoardQuery(articleModel.BoardId);
			GetBoardModel board = await getBoardQuery.DefaultIfExceptionAsync(Mediator);

			var getUserQuery = new GetPublicUserQuery(articleModel.UserId);
			GetPublicUserModel user = await getUserQuery.DefaultIfExceptionAsync(Mediator);

			var privateUser = await new GetAuthenticatedUserQuery().DefaultIfExceptionAsync(Mediator);

			var loggedIn = privateUser != null && privateUser.Id != 0;

			var savedArticle = loggedIn && privateUser.SavedArticles.Contains(id);

			var wroteArticle = loggedIn && privateUser.ArticleIds.Contains(id);

			string imageDataURL = "";
			if (articleModel.AssociatedImageId > 0)
			{
				GetImageModel img = await Mediator.Send(new GetImageByIdQuery(articleModel.AssociatedImageId));
				imageDataURL = _imageDataHelper.ConvertImageDataToDataUrl(img.ImageData, img.ContentType);
			}

			var viewModel = new ArticleDetailsViewModel
			{
				Article = articleModel,
				Board = board,
				CommentPage = new FrontendPage<GetCommentModel>(articleComments),
				LoggedIn = loggedIn,
				User = user,
				UserSavedArticle = savedArticle,
				UserWroteArticle = wroteArticle,
				AssociatedImageDataUrl = imageDataURL
			};

			return View(viewModel);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> AddComment(ArticleDetailsViewModel viewModel)
		{
			if (!ModelState.IsValid) return RedirectToAction("Details", new { Id = viewModel.Article.Id });

			var comment = viewModel.PostCommentModel;
			comment.ParentArticleId = viewModel.Article.Id;

			await Mediator.Send(new AddCommentCommand(comment));

			return RedirectToAction("Details", new { id = viewModel.Article.Id });
		}

		[Authorize]
		public async Task<ActionResult> Vote(int id, bool upvote)
		{
			await Mediator.Send(new VoteArticleCommand(id, upvote));
			return RedirectToAction("Details", new { id });
		}

		[Authorize]
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

		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			await Mediator.Send(new DeleteArticleCommand(id));
			return RedirectToAction("Details", new { id });
		}
	}
}
