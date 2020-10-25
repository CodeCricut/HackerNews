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

namespace HackerNews.Application.Comments.Commands.AddComment
{
	public class AddCommentCommand : IRequest<GetCommentModel>
	{
		public AddCommentCommand(PostCommentModel postCommentModel)
		{
			PostCommentModel = postCommentModel;
		}

		public PostCommentModel PostCommentModel { get; }
	}

	public class AddCommentCommandHandler : DatabaseRequestHandler<AddCommentCommand, GetCommentModel>
	{
		public AddCommentCommandHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetCommentModel> Handle(AddCommentCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());
				if (user == null) throw new UnauthorizedException("Unable to add article; User is not logged in.");

				Comment comment = Mapper.Map<Comment>(request.PostCommentModel);
				comment.PostDate = DateTime.Now;
				comment.UserId = user.Id;

				var addedComment = await UnitOfWork.Comments.AddEntityAsync(comment);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetCommentModel>(addedComment);
			}
		}
	}
}
