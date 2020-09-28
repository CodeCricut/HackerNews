using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Comments;
using HackerNews.EF.Repositories;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class CommentHelper : EntityHelper<Comment, PostCommentModel, GetCommentModel>, IVoteableEntityHelper<Comment>
	{
		public CommentHelper(IEntityRepository<Comment> entityRepository, IMapper mapper)
			: base(entityRepository, mapper)
		{
		}

		public override async Task<GetCommentModel> PostEntityModelAsync(PostCommentModel entityModel, User currentUser)
		{
			var entity = _mapper.Map<Comment>(entityModel);

			entity.UserId = currentUser.Id;

			var addedEntity = await _entityRepository.AddEntityAsync(entity);
			await _entityRepository.SaveChangesAsync();

			return _mapper.Map<GetCommentModel>(addedEntity);
		}

		public override async Task<GetCommentModel> PutEntityModelAsync(int id, PostCommentModel entityModel, User currentUser)
		{
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

		public async Task VoteEntityAsync(int id, bool upvote, User currentUser)
		{
			var comment = await _entityRepository.GetEntityAsync(id);
			// GetEntityAsync(id);
			comment.Karma = upvote
				? comment.Karma + 1
				: comment.Karma - 1;

			await _entityRepository.UpdateEntityAsync(id, comment);
			await _entityRepository.SaveChangesAsync();
		}
	}
}
