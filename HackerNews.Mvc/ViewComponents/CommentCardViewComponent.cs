using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Comments.Queries.GetComment;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Services;
using HackerNews.Mvc.Services.Interfaces;
using HackerNews.Mvc.ViewModels.ViewComponents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewComponents
{
	public class CommentCardViewComponent : ViewComponent
	{
		private readonly IMediator _mediator;
		private readonly IApiJwtManager _apiJwtManager;

		public CommentCardViewComponent(IMediator mediator, IApiJwtManager apiJwtManager)
		{
			_mediator = mediator;
			_apiJwtManager = apiJwtManager;
		}

		public async Task<IViewComponentResult> InvokeAsync(GetCommentModel commentModel)
		{
			var board = await _mediator.Send(new GetBoardQuery(commentModel.BoardId));

			GetArticleModel parentArticle;
			GetCommentModel parentComment;
			try
			{
				parentArticle = await _mediator.Send(new GetArticleQuery(commentModel.ParentArticleId));
			}
			catch
			{
				parentArticle = new GetArticleModel();
			}

			try
			{
				parentComment = await _mediator.Send(new GetCommentQuery(commentModel.ParentCommentId));
			}
			catch
			{
				parentComment = new GetCommentModel();
			}


			GetPublicUserModel user = new GetPublicUserModel();
			try
			{
				user = await _mediator.Send(new GetPublicUserQuery(commentModel.UserId));
			}
			catch { }


			string jwt = "";
			try
			{
				jwt = _apiJwtManager.GetToken();
			}
			catch (System.Exception)
			{
				jwt = "";
			}

			bool loggedIn = false;
			bool saved = false;
			bool userUpvoted = false;
			bool userDownvoted = false;
			bool userCreatedComment = false;
			try
			{
				var loggedInUser = await _mediator.Send(new GetAuthenticatedUserQuery());
				if (loggedInUser != null) loggedIn = true;
				saved = loggedInUser.SavedComments.Any(commentId => commentId == commentModel.Id);
				userUpvoted = loggedInUser.LikedComments.Any(commentId => commentId == commentModel.Id);
				userDownvoted = loggedInUser.DislikedComments.Any(commentId => commentId == commentModel.Id);
				userCreatedComment = commentModel.UserId == loggedInUser.Id;
			}
			catch (System.Exception)
			{
				loggedIn = false;
			}


			var model = new CommentCardViewModel
			{
				Comment = commentModel,
				Board = board,
				ParentArticle = parentArticle,
				ParentComment = parentComment,
				User = user,

				Jwt = jwt,
				LoggedIn = loggedIn,
				Saved = saved,
				UserUpvoted = userUpvoted,
				UserDownvoted = userDownvoted,
				UserCreatedComment = userCreatedComment
			};
			return View(model);
		}
	}
}
