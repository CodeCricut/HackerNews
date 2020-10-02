using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Comments;
using HackerNews.EF.Repositories;
using System;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public abstract class CommentService : EntityService<Comment, PostCommentModel, GetCommentModel>, IVoteableEntityService<Comment>
	{
		public CommentService(IEntityRepository<Comment> entityRepository, IMapper mapper)
			: base(entityRepository, mapper)
		{
		}

		public override async Task<GetCommentModel> PostEntityModelAsync(PostCommentModel entityModel, User currentUser)
		{
			// TODO: add checks to the parents
			//		verify one and only one parent exists
			//		verify parent belongs to boardid/board exists
			var entity = _mapper.Map<Comment>(entityModel);

			entity.UserId = currentUser.Id;
			entity.PostDate = DateTime.UtcNow;

			var addedEntity = await _entityRepository.AddEntityAsync(entity);
			await _entityRepository.SaveChangesAsync();

			return _mapper.Map<GetCommentModel>(addedEntity);
		}

		public override async Task<GetCommentModel> PutEntityModelAsync(int id, PostCommentModel entityModel, User currentUser)
		{
			// TODO: verify parents are the same as previously

			// verify entity trying to update exists
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();
			// verify user owns the entity
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Comment>(entityModel);

			// update and save
			await _entityRepository.UpdateEntityAsync(id, updatedEntity);
			await _entityRepository.SaveChangesAsync();

			// return updated entity
			return await GetEntityModelAsync(id);
		}

		public override async Task SoftDeleteEntityAsync(int id, User currentUser)
		{
			// verify entity exists
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user owns the entity
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _entityRepository.SoftDeleteEntityAsync(id);
			await _entityRepository.SaveChangesAsync();
		}

		public virtual async Task VoteEntityAsync(int id, bool upvote, User currentUser)
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
			} else
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
