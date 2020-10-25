using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
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
		private readonly ICurrentUserService _currentUserService;

		public VoteCommentHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<GetCommentModel> Handle(VoteCommentCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
				var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

				if (!await UnitOfWork.Comments.EntityExistsAsync(request.CommentId)) throw new NotFoundException();
				var comment = await UnitOfWork.Comments.GetEntityAsync(request.CommentId);

				if (request.Upvote)
				{
					// if the user has liked the entity, unlike it (not dislike)
					if (UserLikedEntity(currentUser, comment))
						UnlikeEntity(currentUser, comment);
					// if the user hasn't liked the entity, like it
					else
					{
						// if the user dislike the entity, un-dislike it then like it
						if (UserDislikedEntity(currentUser, comment))
							UndislikeEntity(currentUser, comment);
						LikeEntity(currentUser, comment);
					}
				}
				else
				{
					// if the user has dislike the entity, un-dislike it (not like)
					if (UserDislikedEntity(currentUser, comment))
						UndislikeEntity(currentUser, comment);
					// if the user hasn't disliked the entity, dislike it
					else
					{
						// if the user liked the entity, unlike it then dislike it
						if (UserLikedEntity(currentUser, comment))
							UnlikeEntity(currentUser, comment);
						DislikeEntity(currentUser, comment);
					}
				}

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetCommentModel>(comment);
			}
		}

		private static bool UserDislikedEntity(User currentUser, Comment comment)
		{
			return comment.UsersDisliked.FirstOrDefault(ud => ud.UserId == currentUser.Id) != null;
		}

		private static bool UserLikedEntity(User currentUser, Comment comment)
		{
			return comment.UsersLiked.FirstOrDefault(ul => ul.UserId == currentUser.Id) != null;
		}

		private static void UndislikeEntity(User currentUser, Comment comment)
		{
			comment.Karma++;
			var joinEntity = comment.UsersDisliked.FirstOrDefault(ud => ud.UserId == currentUser.Id);

			comment.UsersDisliked.Remove(joinEntity);
			currentUser.DislikedComments.Remove(joinEntity);
		}

		private static void DislikeEntity(User currentUser, Comment comment)
		{
			var userDislike = new UserCommentDislikes
			{
				Comment = comment,
				User = currentUser
			};

			comment.Karma--;

			comment.UsersDisliked.Add(userDislike);
			currentUser.DislikedComments.Add(userDislike);
		}

		private static void UnlikeEntity(User currentUser, Comment comment)
		{
			comment.Karma--;
			var joinEntity = comment.UsersLiked.FirstOrDefault(ul => ul.UserId == currentUser.Id);

			comment.UsersLiked.Remove(joinEntity);
			currentUser.LikedComments.Remove(joinEntity);
		}

		private static void LikeEntity(User currentUser, Comment comment)
		{
			var userLike = new UserCommentLikes
			{
				Comment = comment,
				User = currentUser
			};

			comment.Karma++;

			comment.UsersLiked.Add(userLike);
			currentUser.LikedComments.Add(userLike);
		}
	}
}
