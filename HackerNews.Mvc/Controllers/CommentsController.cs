using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Comments.Commands.AddComment;
using HackerNews.Application.Comments.Commands.DeleteComment;
using HackerNews.Application.Comments.Commands.UpdateComment;
using HackerNews.Application.Comments.Commands.VoteComment;
using HackerNews.Application.Comments.Queries.GetComment;
using HackerNews.Application.Comments.Queries.GetCommentsByIds;
using HackerNews.Application.Comments.Queries.GetCommentsBySearch;
using HackerNews.Application.Users.Commands.SaveCommentToUser;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Models;
using HackerNews.Mvc.ViewModels.Comments;
using HackerNews.Mvc.ViewModels.ViewComponents;
using HackerNews.Web.Pipeline.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Controllers
{
	public class CommentsController : FrontendController
	{
		public async Task<ViewResult> Details(int id, PagingParams pagingParams)
		{
			var commentModel = await Mediator.Send(new GetCommentQuery(id));
			var board = await Mediator.Send(new GetBoardQuery(commentModel.BoardId));

			GetArticleModel parentArticle;
			try
			{
				parentArticle = await Mediator.Send(new GetArticleQuery(commentModel.ParentArticleId));
			}
			catch (NotFoundException ex)
			{
				parentArticle = new GetArticleModel();
			}

			GetCommentModel parentComment;
			try
			{
				parentComment = await Mediator.Send(new GetCommentQuery(commentModel.ParentCommentId));
			}
			catch (NotFoundException ex)
			{
				parentComment = new GetCommentModel();
			}

			var childComments = await Mediator.Send(new GetCommentsByIdsQuery(commentModel.CommentIds, pagingParams));
			var user = await Mediator.Send(new GetPublicUserQuery(commentModel.UserId));

			GetPrivateUserModel privateUser = null;
			try
			{
				privateUser = await Mediator.Send(new GetAuthenticatedUserQuery());
			}
			catch (NotFoundException) { }
			catch (UnauthorizedException) { }

			var loggedIn = privateUser != null && privateUser.Id != 0;
			var savedComment = loggedIn
				? privateUser.SavedComments.Contains(id)
				: false;


			var model = new CommentDetailsViewModel
			{
				Board = board,
				ChildCommentPage = new FrontendPage<GetCommentModel>(childComments),
				Comment = commentModel,
				LoggedIn = loggedIn,
				ParentArticle = parentArticle,
				ParentComment = parentComment,
				PostCommentModel = new PostCommentModel(),
				User = user,
				UserSavedComment = savedComment,
				UserWroteComment = commentModel.UserId == user.Id
			};
			return View(model);
		}

		[HttpPost]
		[JwtAuthorize]
		public async Task<ActionResult> AddComment(CommentDetailsViewModel viewModel)
		{
			viewModel.PostCommentModel.ParentCommentId = viewModel.Comment.Id;

			await Mediator.Send(new AddCommentCommand(viewModel.PostCommentModel));
			return RedirectToAction("Details", new { id = viewModel.Comment.Id });
		}

		[HttpPost]
		[JwtAuthorize]
		public async Task<ActionResult> Update(CommentCardViewModel viewModel)
		{
			// messy, should be a PostCommentModel on the viewModel
			PostCommentModel updateModel = new PostCommentModel
			{
				Text = viewModel.Comment.Text
			};
			await Mediator.Send(new UpdateCommentCommand(viewModel.Comment.Id, updateModel));
			return RedirectToAction("Details", new { id = viewModel.Comment.Id });
		}

		[JwtAuthorize]
		public async Task<ActionResult> Vote(int id, bool upvote)
		{
			await Mediator.Send(new VoteCommentCommand(id, upvote));
			return RedirectToAction("Details", new { id });
		}

		[JwtAuthorize]
		public async Task<ActionResult> SaveComment(int id)
		{
			await Mediator.Send(new SaveCommentToUserCommand(id));
			return RedirectToAction("Details", new { id });
		}

		public async Task<ActionResult<CommentSearchViewModel>> Search(string searchTerm, PagingParams pagingParams)
		{
			var comments = await Mediator.Send(new GetCommentsBySearchQuery(searchTerm, pagingParams));
			var model = new CommentSearchViewModel { SearchTerm = searchTerm, CommentPage = new FrontendPage<GetCommentModel>(comments) };
			return View(model);
		}

		[JwtAuthorize]
		public async Task<IActionResult> Delete(int id)
		{
			await Mediator.Send(new DeleteCommentCommand(id));
			return RedirectToAction("Details", new { id });
		}
	}
}
