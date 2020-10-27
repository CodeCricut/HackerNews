using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Comments.Queries.GetComment;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.ViewModels.ViewComponents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewComponents
{
	public class CommentCardViewComponent : ViewComponent
	{
		private readonly IMediator _mediator;

		public CommentCardViewComponent(IMediator mediator)
		{
			_mediator = mediator;
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
			try
			{
				parentComment = await _mediator.Send(new GetCommentQuery(commentModel.ParentCommentId));
			}
			catch (NotFoundException ex)
			{
				parentComment = new GetCommentModel();
			}


			var user = await _mediator.Send(new GetPublicUserQuery(commentModel.UserId));

			var model = new CommentCardViewModel
			{
				Comment = commentModel,
				Board = board,
				ParentArticle = parentArticle,
				ParentComment = parentComment,
				User = user
			};
			return View(model);
		}
	}
}
