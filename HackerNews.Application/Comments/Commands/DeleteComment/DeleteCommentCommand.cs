using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Comments.Commands.DeleteComment
{
	public class DeleteCommentCommand : IRequest<bool>
	{
		public DeleteCommentCommand(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class DeleteCommentHandler : DatabaseRequestHandler<DeleteCommentCommand, bool>
	{
		public DeleteCommentHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
		{
				if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
				var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

				// Verify entity exists.
				if (!await UnitOfWork.Comments.EntityExistsAsync(request.Id)) throw new NotFoundException();

				// Verify user owns the entity.
				var comment = await UnitOfWork.Comments.GetEntityAsync(request.Id);
				if (comment.UserId != currentUser.Id) throw new UnauthorizedException();

				// soft delete and save
				var successful = await UnitOfWork.Comments.DeleteEntityAsync(request.Id);
				UnitOfWork.SaveChanges();

				return successful;
		}
	}
}
