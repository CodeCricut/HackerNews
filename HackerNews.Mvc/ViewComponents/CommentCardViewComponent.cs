using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Comments.Queries.GetComment;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Exceptions;
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
		private readonly IJwtSetterService _jwtService;

		public CommentCardViewComponent(IMediator mediator, IJwtSetterService jwtService)
		{
			_mediator = mediator;
			_jwtService = jwtService;
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
			catch (NotFoundException ex)
			{
				parentArticle = new GetArticleModel();
			}
			catch (EntityDeletedException)
			{
				parentArticle = new GetArticleModel();
			}

			try
			{
				parentComment = await _mediator.Send(new GetCommentQuery(commentModel.ParentCommentId));
			}
			catch (NotFoundException)
			{
				parentComment = new GetCommentModel();
			}
			catch (EntityDeletedException)
			{
				parentComment = new GetCommentModel();
			}


			var user = await _mediator.Send(new GetPublicUserQuery(commentModel.UserId));


			string jwt = "";
			try
			{
				jwt = _jwtService.GetToken();
			}
			catch (System.Exception e)
			{
				jwt = "";
			}

			bool loggedIn = true;
			bool saved = false;
			bool userUpvoted = false;
			bool userDownvoted = false;
			bool userCreatedComment = false;
			try
			{
				var loggedInUser = await _mediator.Send(new GetAuthenticatedUserQuery());
				if (loggedInUser == null) loggedIn = false;
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
