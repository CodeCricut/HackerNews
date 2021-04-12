using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
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
		private readonly IAdminLevelOperationValidator<Comment> _commentOperationValidator;

		public DeleteCommentHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService,
			IAdminLevelOperationValidator<Comment> commentOperationValidator) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_commentOperationValidator = commentOperationValidator;
		}

		public override async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
			var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			// Verify entity exists.
			if (!await UnitOfWork.Comments.EntityExistsAsync(request.Id)) throw new NotFoundException();

			// Verify user owns the entity.
			var comment = await UnitOfWork.Comments.GetEntityAsync(request.Id);

			bool userOwnsComment = comment.UserId != currentUser.Id;
			bool userModeratesComment = await _commentOperationValidator.CanDeleteEntityAsync(comment, currentUser.AdminLevel);
			if (!(userOwnsComment || userModeratesComment)) throw new UnauthorizedException();

			// soft delete and save
			var successful = await UnitOfWork.Comments.DeleteEntityAsync(request.Id);
			UnitOfWork.SaveChanges();

			return successful;
		}
	}
}
