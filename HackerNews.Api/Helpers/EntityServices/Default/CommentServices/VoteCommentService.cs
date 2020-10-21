using AutoMapper;
using CleanEntityArchitecture.Authorization;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.CommentServices
{
	public class VoteCommentService : VoteEntityService<Comment>
	{
		private readonly IMapper _mapper;
		private readonly IWriteEntityRepository<Comment> _writeRepo;
		private readonly IReadEntityRepository<Comment> _readRepo;
		private readonly IUserAuth<User> _cleanUserAuth;

		public VoteCommentService(IMapper mapper,
			IWriteEntityRepository<Comment> writeRepo,
			IReadEntityRepository<Comment> readRepo,
			IUserAuth<User> cleanUserAuth)
		{
			_mapper = mapper;
			_writeRepo = writeRepo;
			_readRepo = readRepo;
			_cleanUserAuth = cleanUserAuth;
		}

		public override async Task VoteEntityAsync(int id, bool upvote)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();

			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			var comment = await _readRepo.GetEntityAsync(id);

			if (upvote)
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
			await _readRepo.SaveChangesAsync();
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
