using AutoMapper;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.Domain.Parameters;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base
{
	public abstract class ReadEntityService<TEntity, TGetModel> : IReadEntityService<TEntity, TGetModel>
		where TEntity : DomainEntity
		where TGetModel : GetEntityModel
	{
		private readonly IMapper _mapper;
		private readonly IEntityRepository<TEntity> _entityRepository;

		public ReadEntityService(IMapper mapper, IEntityRepository<TEntity> entityRepository)
		{
			_mapper = mapper;
			_entityRepository = entityRepository;
		}

		public virtual async Task<PagedList<TGetModel>> GetAllEntityModelsAsync(PagingParams pagingParams)
		{
			var entityPagedList = (await _entityRepository.GetEntitiesAsync(pagingParams));

			// convert to list of models
			List<TEntity> entityList = entityPagedList.ToList();
			var entityModelList = _mapper.Map<List<TGetModel>>(entityList);

			// return paged list of models
			return new PagedList<TGetModel>(entityModelList, entityPagedList.Count, pagingParams);
		}

		public virtual async Task<TGetModel> GetEntityModelAsync(int id)
		{
			TEntity entity = await _entityRepository.GetEntityAsync(id);

			return _mapper.Map<TGetModel>(entity);
		}
	}
}
