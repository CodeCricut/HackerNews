using AutoMapper;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters
{
	public abstract class EntityConverter<EntityT, PostModelT, GetModelT>
		where EntityT : DomainEntity
		where PostModelT : PostEntityModel
		where GetModelT : GetEntityModel
	{
		protected readonly IMapper _mapper;

		public EntityConverter(IMapper mapper)
		{
			_mapper = mapper;
		}

		public abstract  Task<EntityT> ConvertEntityModelAsync(PostModelT entityModel);
		public  async Task<List<EntityT>> ConvertEntityModelsAsync(List<PostModelT> entityModels)
		{
			return await TaskHelper.RunConcurrentTasksAsync(entityModels, 
				entityModel => ConvertEntityModelAsync(entityModel));
		}

		public abstract Task<DestinationT> ConvertEntityAsync<DestinationT>(EntityT entity);
		public  async Task<List<DestinationT>> ConvertEntitiesAsync<DestinationT>(List<EntityT> entities)
		{
			return await TaskHelper.RunConcurrentTasksAsync(entities,
				entity => ConvertEntityAsync<DestinationT>(entity));
		}
	}
}
