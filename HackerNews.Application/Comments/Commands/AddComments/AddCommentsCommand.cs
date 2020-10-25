using HackerNews.Application.Common.Interfaces;
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
		public AddCommentsCommand(IEnumerable<PostCommentModel> postCommentModels)
		{
			PostCommentModels = postCommentModels;
		}

		public IEnumerable<PostCommentModel> PostCommentModels { get; }
	}

	public class AddCommentHandler : DatabaseRequestHandler<AddCommentsCommand, IEnumerable<GetCommentModel>>
	{
		private readonly ICurrentUserService _currentUserService;

		public AddCommentHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<IEnumerable<GetCommentModel>> Handle(AddCommentsCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
				var user = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

				var commentsToAdd = new List<Comment>();
				foreach(var postCommentModel in request.PostCommentModels)
				{
					Comment comment = Mapper.Map<Comment>(postCommentModel);

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

					commentsToAdd.Add(comment);
				}

				var addedComments = await UnitOfWork.Comments.AddEntititesAsync(commentsToAdd);

				UnitOfWork.SaveChanges();

				return Mapper.Map<IEnumerable<GetCommentModel>>(addedComments);
			}
		}
	}
}
