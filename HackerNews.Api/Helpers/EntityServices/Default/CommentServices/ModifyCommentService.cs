using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Comments;
using HackerNews.EF.Repositories;
using System;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.CommentServices
{
	public class ModifyCommentService : ModifyEntityService<Comment, PostCommentModel, GetCommentModel>
	{
		private readonly IEntityRepository<Comment> _entityRepository;
		private readonly IMapper _mapper;

		public ModifyCommentService(IEntityRepository<Comment> entityRepository, IMapper mapper)
		{
			_entityRepository = entityRepository;
			_mapper = mapper;
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
			// TODO: ddd
			return _mapper.Map<GetCommentModel>(updatedEntity);
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
	}
}
