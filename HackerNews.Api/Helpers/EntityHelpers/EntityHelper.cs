using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF.Repositories;
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
		protected readonly IEntityRepository<EntityT> _entityRepository;
		protected readonly IMapper _mapper;

		public EntityHelper(IEntityRepository<EntityT> entityRepository, IMapper mapper)
		{
			_entityRepository = entityRepository;
			_mapper = mapper;
		}

		// TODO: we still need to check parents exist on an entity basis...
		public virtual async Task<GetModelT> PostEntityModelAsync(PostModelT entityModel)
		{
			EntityT entity = _mapper.Map<EntityT>(entityModel);
			//_entityConverter.ConvertEntityModelAsync(entityModel);

			var addedEntity = await _entityRepository.AddEntityAsync(entity);
			await _entityRepository.SaveChangesAsync();

			return _mapper.Map<GetModelT>(addedEntity);
			// _entityConverter.ConvertEntityAsync<GetModelT>(addedEntity);
		}

		public async Task PostEntityModelsAsync(List<PostModelT> entityModels)
		{
			List<EntityT> entities = _mapper.Map<List<EntityT>>(entityModels);
			//_entityConverter.ConvertEntityModelsAsync(entityModels);
			await _entityRepository.AddEntititesAsync(entities);
			await _entityRepository.SaveChangesAsync();
		}

		public async Task<GetModelT> PutEntityModelAsync(int id, PostModelT entityModel)
		{
			var convertedModel = _mapper.Map<EntityT>(entityModel);

			// var entity = _mapper.Map<EntityT, EntityT>(convertedModel);

			await _entityRepository.UpdateEntityAsync(id, convertedModel);
			await _entityRepository.SaveChangesAsync();

			return await GetEntityModelAsync(id);
		}

		public async Task SoftDeleteEntityAsync(int id)
		{
			// code smell: verify the entity exists in the DB
			// await GetEntityAsync(id);

			await _entityRepository.SoftDeleteEntityAsync(id);
			await _entityRepository.SaveChangesAsync();
		}

		//public async Task<EntityT> GetEntityAsync(int id)
		//{
		//	var entity = await _entityRepository.GetEntityAsync(id);
		//	if (entity == null) throw new NotFoundException("An entity with the given ID could not be found.");

		//	return entity;
		//}

		// public abstract void UpdateEntityProperties(EntityT entity, EntityT newEntity);

		// public abstract Task<List<EntityT>> GetAllEntitiesAsync();


		public virtual async Task<GetModelT> GetEntityModelAsync(int id)
		{
			EntityT entity = await _entityRepository.GetEntityAsync(id);

			return _mapper.Map<GetModelT>(entity);
		}

		// TODO: I believe we need to trim the entities before converting them.
		public async Task<List<GetModelT>> GetAllEntityModelsAsync()
		{
			try
			{
				List<EntityT> entities = (await _entityRepository.GetEntitiesAsync()).ToList();
				// var entities = await GetAllEntitiesAsync();

				return _mapper.Map<List<GetModelT>>(entities);
				//await _entityConverter.ConvertEntitiesAsync<GetModelT>(entities);
			}
			catch (System.Exception e)
			{

				throw;
			}
			
		}
	}
}
