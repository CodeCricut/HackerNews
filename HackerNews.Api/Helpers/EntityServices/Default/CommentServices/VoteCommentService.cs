using AutoMapper;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.CommentServices
{
	public class VoteCommentService : VoteEntityService<Comment>
	{
		private readonly IMapper _mapper;
		private readonly IWriteEntityRepository<Comment> _writeRepo;
		private readonly IReadEntityRepository<Comment> _readRepo;
		private readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuth;

		public VoteCommentService(IMapper mapper,
			IWriteEntityRepository<Comment> writeRepo,
			IReadEntityRepository<Comment> readRepo,
			IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuth)
		{
			_mapper = mapper;
			_writeRepo = writeRepo;
			_readRepo = readRepo;
			_userAuth = userAuth;
		}

		public override async Task VoteEntityAsync(int id, bool upvote)
		{
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			var currentUser = await _userAuth.GetAuthenticatedUser();

			var comment = await _readRepo.GetEntityAsync(id);

			if (upvote)
			{
				var userLike = new UserCommentLikes
				{
					User = currentUser,
					Comment = comment
				};

				comment.Karma++;

				comment.UsersLiked.Add(userLike);
				currentUser.LikedComments.Add(userLike);
			}
			else
			{
				var userDislike = new UserCommentDislikes
				{
					User = currentUser,
					Comment = comment
				};

				comment.Karma--;
				comment.UsersDisliked.Add(userDislike);
				currentUser.DislikedComments.Add(userDislike);
			}

			await _writeRepo.UpdateEntityAsync(id, comment);
			await _writeRepo.SaveChangesAsync();
		}
	}
}
