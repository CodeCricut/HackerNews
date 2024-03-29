﻿using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Linq;
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
		public VoteCommentHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetCommentModel> Handle(VoteCommentCommand request, CancellationToken cancellationToken)
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
			var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			if (!await UnitOfWork.Comments.EntityExistsAsync(request.CommentId)) throw new NotFoundException();
			var comment = await UnitOfWork.Comments.GetEntityAsync(request.CommentId);

			if (!await UnitOfWork.Users.EntityExistsAsync(comment.UserId)) throw new NotFoundException();
			var commentUser = await UnitOfWork.Users.GetEntityAsync(comment.UserId);

			if (request.Upvote)
			{
				// if the user has liked the entity, unlike it (not dislike)
				if (UserLikedEntity(currentUser, comment))
					UnlikeEntity(currentUser, commentUser, comment);
				// if the user hasn't liked the entity, like it
				else
				{
					// if the user dislike the entity, un-dislike it then like it
					if (UserDislikedEntity(currentUser, comment))
						UndislikeEntity(currentUser, commentUser, comment);
					LikeEntity(currentUser, commentUser, comment);
				}
			}
			else
			{
				// if the user has dislike the entity, un-dislike it (not like)
				if (UserDislikedEntity(currentUser, comment))
					UndislikeEntity(currentUser, commentUser, comment);
				// if the user hasn't disliked the entity, dislike it
				else
				{
					// if the user liked the entity, unlike it then dislike it
					if (UserLikedEntity(currentUser, comment))
						UnlikeEntity(currentUser, commentUser, comment);
					DislikeEntity(currentUser, commentUser, comment);
				}
			}

			UnitOfWork.SaveChanges();

			return Mapper.Map<GetCommentModel>(comment);
		}

		private static bool UserDislikedEntity(User currentUser, Comment comment)
		{
			return comment.UsersDisliked.FirstOrDefault(ud => ud.UserId == currentUser.Id) != null;
		}

		private static bool UserLikedEntity(User currentUser, Comment comment)
		{
			return comment.UsersLiked.FirstOrDefault(ul => ul.UserId == currentUser.Id) != null;
		}

		private static void UndislikeEntity(User currentUser, User commentUser, Comment comment)
		{
			comment.Karma++;

			commentUser.Karma++;

			var joinEntity = comment.UsersDisliked.FirstOrDefault(ud => ud.UserId == currentUser.Id);

			comment.UsersDisliked.Remove(joinEntity);
			currentUser.DislikedComments.Remove(joinEntity);
		}

		private static void DislikeEntity(User currentUser, User commentUser, Comment comment)
		{
			var userDislike = new UserCommentDislikes
			{
				Comment = comment,
				User = currentUser
			};

			comment.Karma--;

			commentUser.Karma--;

			comment.UsersDisliked.Add(userDislike);
			currentUser.DislikedComments.Add(userDislike);
		}

		private static void UnlikeEntity(User currentUser, User commentUser, Comment comment)
		{
			comment.Karma--;

			commentUser.Karma--;

			var joinEntity = comment.UsersLiked.FirstOrDefault(ul => ul.UserId == currentUser.Id);

			comment.UsersLiked.Remove(joinEntity);
			currentUser.LikedComments.Remove(joinEntity);
		}

		private static void LikeEntity(User currentUser, User commentUser, Comment comment)
		{
			var userLike = new UserCommentLikes
			{
				Comment = comment,
				User = currentUser
			};

			comment.Karma++;

			commentUser.Karma++;

			comment.UsersLiked.Add(userLike);
			currentUser.LikedComments.Add(userLike);
		}
	}
}
