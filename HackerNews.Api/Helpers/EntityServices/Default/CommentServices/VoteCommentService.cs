using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.CommentServices
{
	public class VoteCommentService : VoteEntityService<Comment>
	{
		private readonly IEntityRepository<Comment> _entityRepository;
		private readonly IMapper _mapper;

		public VoteCommentService(IEntityRepository<Comment> entityRepository, IMapper mapper)
		{
			_entityRepository = entityRepository;
			_mapper = mapper;
		}

		public override async Task VoteEntityAsync(int id, bool upvote, User currentUser)
		{
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();

			var comment = await _entityRepository.GetEntityAsync(id);

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

			await _entityRepository.UpdateEntityAsync(id, comment);
			await _entityRepository.SaveChangesAsync();
		}
	}
}
