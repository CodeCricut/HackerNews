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

namespace HackerNews.Application.Comments.Commands.AddSubComment
{
	public class AddCommentSubCommentCommand : IRequest<GetCommentModel>
	{
		public AddCommentSubCommentCommand(int commentId, PostCommentModel postCommentModel)
		{
			CommentId = commentId;
			PostCommentModel = postCommentModel;
		}

		public int CommentId { get; }
		public PostCommentModel PostCommentModel { get; }
	}

	public class AddCommentSubCommentHandler : DatabaseRequestHandler<AddCommentSubCommentCommand, GetCommentModel>
	{
		public AddCommentSubCommentHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetCommentModel> Handle(AddCommentSubCommentCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());
				if (user == null) throw new UnauthorizedException();

				var parentComment = await UnitOfWork.Comments.GetEntityAsync(request.CommentId);
				if (parentComment == null) throw new NotFoundException();

				var childComment = Mapper.Map<Comment>(request.PostCommentModel);
				childComment.UserId = user.Id;
				childComment.PostDate = DateTime.Now;

				var updatedComment = await UnitOfWork.Comments.AddSubComment(request.CommentId, childComment);

				return Mapper.Map<GetCommentModel>(updatedComment);
			}
		}
	}
}
