using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
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
		public AddCommentCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetCommentModel> Handle(AddCommentCommand request, CancellationToken cancellationToken)
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
			var user = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			Comment comment = Mapper.Map<Comment>(request.PostCommentModel);

			var postCommentModel = request.PostCommentModel;
			var parentArticle = await UnitOfWork.Articles.GetEntityAsync(postCommentModel.ParentArticleId);
			var parentComment = await UnitOfWork.Comments.GetEntityAsync(postCommentModel.ParentCommentId);

			if (parentArticle == null &&
				parentComment == null) throw new InvalidPostException("No parent given.");
			if (parentArticle != null &&
				parentComment != null) throw new InvalidPostException("Two parents given.");

			var boardId = parentArticle != null
					? parentArticle.BoardId
					: parentComment.BoardId;


			comment.UserId = user.Id;
			comment.PostDate = DateTime.UtcNow;
			comment.BoardId = boardId;

			var addedComment = await UnitOfWork.Comments.AddEntityAsync(comment);
			UnitOfWork.SaveChanges();

			return Mapper.Map<GetCommentModel>(addedComment);
		}
	}
}
