using AutoMapper;
using HackerNews.Api.Converters;
using HackerNews.Api.Converters.Trimmers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models;
using HackerNews.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public abstract class EntityHelper<EntityT, PostModelT, GetModelT> : IEntityHelper<EntityT, PostModelT, GetModelT>
		where EntityT : DomainEntity
		where PostModelT : PostEntityModel
		where GetModelT : GetEntityModel
	{
		protected readonly EntityRepository<EntityT> _entityRepository;
		protected readonly EntityConverter<EntityT, PostModelT, GetModelT> _entityConverter;

		public EntityHelper(
			EntityRepository<EntityT> entityRepository,
			EntityConverter<EntityT, PostModelT, GetModelT> entityConverter)
		{
			_entityRepository = entityRepository;
			_entityConverter = entityConverter;
		}

		public async Task<GetModelT> PostEntityModelAsync(PostModelT entityModel)
		{
			EntityT entity = await _entityConverter.ConvertEntityModelAsync(entityModel);

			var addedEntity = await _entityRepository.AddEntityAsync(entity);
			await _entityRepository.SaveChangesAsync();

			return await _entityConverter.ConvertEntityAsync<GetModelT>(addedEntity);
		}

		public async Task PostEntityModelsAsync(List<PostModelT> entityModels)
		{
			List<EntityT> entities = await _entityConverter.ConvertEntityModelsAsync(entityModels);
			await _entityRepository.AddEntititesAsync(entities);
			await _entityRepository.SaveChangesAsync();
		}

		public abstract Task<GetModelT> GetEntityModelAsync(int id);

		public abstract Task<List<GetModelT>> GetAllEntityModelsAsync();

		public async Task<GetModelT> PutEntityModelAsync(int id, PostModelT entityModel)
		{
			var entity = await GetEntityAsync(id);

			UpdateEntityProperties(entity, entityModel);

			await _entityRepository.UpdateEntityAsync(id, entity);
			await _entityRepository.SaveChangesAsync();


			return await GetEntityModelAsync(id);
		}

		// this should only be implemented in some helpes, such as comments and articles. I am leaving it hear simply to remember.
		// public abstract Task VoteEntityAsync(int it, bool upvote);

		public async Task SoftDeleteEntityAsync(int id)
		{
			// code smell: verify the entity exists in the DB
			await GetEntityAsync(id);

			await _entityRepository.SoftDeleteEntityAsync(id);
			await _entityRepository.SaveChangesAsync();
		}

		public async Task<EntityT> GetEntityAsync(int id)
		{
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity == null) throw new NotFoundException("An entity with the given ID could not be found.");

			return entity;
		}

		public abstract void UpdateEntityProperties(EntityT entity, PostModelT entityModel);

		public abstract Task<List<EntityT>> GetAllEntitiesAsync();
	}
}
