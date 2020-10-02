using AutoMapper;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.Domain.Parameters;
using HackerNews.EF.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public abstract class EntityService<EntityT, PostModelT, GetModelT> : IEntityService<EntityT, PostModelT, GetModelT>
		where EntityT : DomainEntity
		where PostModelT : PostEntityModel
		where GetModelT : GetEntityModel
	{
		protected readonly IEntityRepository<EntityT> _entityRepository;
		protected readonly IMapper _mapper;

		public EntityService(IEntityRepository<EntityT> entityRepository, IMapper mapper)
		{
			_entityRepository = entityRepository;
			_mapper = mapper;
		}

		public abstract Task<GetModelT> PostEntityModelAsync(PostModelT entityModel, User currentUser);

		public virtual async Task PostEntityModelsAsync(List<PostModelT> entityModels, User currentUser)
		{
			await TaskHelper.RunConcurrentTasksAsync(entityModels, async entityModel => await PostEntityModelAsync(entityModel, currentUser));
		}

		public abstract Task<GetModelT> PutEntityModelAsync(int id, PostModelT entityModel, User currentUser);

		public abstract Task SoftDeleteEntityAsync(int id, User currentUser);

		public virtual async Task<GetModelT> GetEntityModelAsync(int id)
		{
			EntityT entity = await _entityRepository.GetEntityAsync(id);

			return _mapper.Map<GetModelT>(entity);
		}

		public virtual async Task<List<GetModelT>> GetAllEntityModelsAsync(PagingParams pagingParams)
		{
			// Instead of returning the list of entity models, as PagedList<TEntity> could be returned here.
			List<EntityT> entities = (await _entityRepository.GetEntitiesAsync(pagingParams)).ToList();

			return _mapper.Map<List<GetModelT>>(entities);
		}
	}
}
