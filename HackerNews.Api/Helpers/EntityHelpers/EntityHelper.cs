using AutoMapper;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
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

		public abstract Task<GetModelT> PostEntityModelAsync(PostModelT entityModel, User currentUser);
		//{
		//	EntityT entity = _mapper.Map<EntityT>(entityModel);
		//	//_entityConverter.ConvertEntityModelAsync(entityModel);

		//	var addedEntity = await _entityRepository.AddEntityAsync(entity);
		//	await _entityRepository.SaveChangesAsync();

		//	return _mapper.Map<GetModelT>(addedEntity);
		//	// _entityConverter.ConvertEntityAsync<GetModelT>(addedEntity);
		//}

		public async Task PostEntityModelsAsync(List<PostModelT> entityModels, User currentUser)
		{
			// Rather slow in terms of database connection, but clean so...
			await TaskHelper.RunConcurrentTasksAsync(entityModels, async entityModel => await PostEntityModelAsync(entityModel, currentUser));
		}

		// implement on per entkty basis. for example, you can't change the parents of an entity
		public abstract Task<GetModelT> PutEntityModelAsync(int id, PostModelT entityModel, User currentUser);
		//{
		//	var convertedModel = _mapper.Map<EntityT>(entityModel);

		//	// var entity = _mapper.Map<EntityT, EntityT>(convertedModel);

		//	await _entityRepository.UpdateEntityAsync(id, convertedModel);
		//	await _entityRepository.SaveChangesAsync();

		//	return await GetEntityModelAsync(id);
		//}

		// have to implement on per entity basis so we can verify user owns entity
		public abstract Task SoftDeleteEntityAsync(int id, User currentUser);
		//{
		//	if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();
		//	var entity = await _entityRepository.GetEntityAsync(id);
		//	if (entity.)

		//	await _entityRepository.SoftDeleteEntityAsync(id);
		//	await _entityRepository.SaveChangesAsync();
		//}

		public virtual async Task<GetModelT> GetEntityModelAsync(int id)
		{
			EntityT entity = await _entityRepository.GetEntityAsync(id);

			return _mapper.Map<GetModelT>(entity);
		}

		public virtual async Task<List<GetModelT>> GetAllEntityModelsAsync()
		{
			List<EntityT> entities = (await _entityRepository.GetEntitiesAsync()).ToList();

			return _mapper.Map<List<GetModelT>>(entities);
		}
	}
}
