using AutoMapper;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Comments.Commands.VoteComment
{
	public class VoteCommentCommand : IRequest<GetCommentModel>
	{
		public VoteCommentCommand(int commentId, bool upvote)
		{
			CommentId = commentId;
			Upvote = upvote;
		}

		public int CommentId { get; }
		public bool Upvote { get; }
	}

	public class VoteCommentHandler : DatabaseRequestHandler<VoteCommentCommand, GetCommentModel>
	{
		public VoteCommentHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetCommentModel> Handle(VoteCommentCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());
				if (user == null) throw new UnauthorizedException();

				var comment = await UnitOfWork.Comments.GetEntityAsync(request.CommentId);
				if (comment == null) throw new NotFoundException();

				var updatedComment = await UnitOfWork.Comments.VoteComment(request.CommentId, request.Upvote);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetCommentModel>(updatedComment);
			}
		}
	}
}
