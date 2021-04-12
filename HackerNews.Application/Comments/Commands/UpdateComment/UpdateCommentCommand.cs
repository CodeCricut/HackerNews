using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Comments.Commands.UpdateComment
{
	public class UpdateCommentCommand : IRequest<GetCommentModel>
	{
		public UpdateCommentCommand(int commentId, PostCommentModel postCommentModel)
		{
			CommentId = commentId;
			PostCommentModel = postCommentModel;
		}

		public int CommentId { get; }
		public PostCommentModel PostCommentModel { get; }
	}

	public class UpdateCommentHandler : DatabaseRequestHandler<UpdateCommentCommand, GetCommentModel>
	{
		private readonly IAdminLevelOperationValidator<Comment> _commentOperationValidator;

		public UpdateCommentHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService,
			IAdminLevelOperationValidator<Comment> commentOperationValidator) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_commentOperationValidator = commentOperationValidator;
		}

		public override async Task<GetCommentModel> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.UserId;
			var currentUser = await UnitOfWork.Users.GetEntityAsync(userId);
			if (currentUser == null) throw new UnauthorizedException();

			// Verify entity trying to update exists.
			if (!await UnitOfWork.Comments.EntityExistsAsync(request.CommentId)) throw new NotFoundException();

			var comment = await UnitOfWork.Comments.GetEntityAsync(request.CommentId);

			// verify user owns the entity
			bool userOwnsComment = comment.UserId != currentUser.Id;
			bool userModeratesComment = await _commentOperationValidator.CanModifyEntityAsync(comment, currentUser.AdminLevel);
			if (!(userOwnsComment || userModeratesComment)) throw new UnauthorizedException();


			var updateModel = request.PostCommentModel;
			comment.Text = updateModel.Text;

			// update and save
			await UnitOfWork.Comments.UpdateEntityAsync(request.CommentId, comment);
			UnitOfWork.SaveChanges();

			return Mapper.Map<GetCommentModel>(comment);
		}
	}
}
