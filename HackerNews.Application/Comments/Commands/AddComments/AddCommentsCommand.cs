using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Comments.Commands.AddComments
{
	public class AddCommentsCommand : IRequest<IEnumerable<GetCommentModel>>
	{
		public AddCommentsCommand(PostCommentModel postCommentModel)
		{
			PostCommentModel = postCommentModel;
		}

		public PostCommentModel PostCommentModel { get; }
	}

	public class AddCommentHandler : DatabaseRequestHandler<AddCommentsCommand, IEnumerable<GetCommentModel>>
	{
		public AddCommentHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<IEnumerable<GetCommentModel>> Handle(AddCommentsCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());
				if (user == null) throw new UnauthorizedException();

				var comments = Mapper.Map<IEnumerable<Comment>>(request.PostCommentModel);
				foreach (var comment in comments)
				{
					comment.PostDate = DateTime.Now;
					comment.UserId = user.Id;
				}

				var addedComments = await UnitOfWork.Comments.AddEntititesAsync(comments);

				return Mapper.Map<IEnumerable<GetCommentModel>>(addedComments);
			}
		}
	}
}
