using AutoMapper;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System.Collections.Generic;
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

		/// <summary>
		/// Conver the given <paramref name="entityModel"/> to a <see cref="EntityT"/>. If the given model
		/// contains any valid parent or child IDs, then the returned entity will have references to them.
		/// </summary>
		/// <param name="entityModel"></param>
		/// <returns></returns>
		public abstract  Task<EntityT> ConvertEntityModelAsync(PostModelT entityModel);
		public  async Task<List<EntityT>> ConvertEntityModelsAsync(List<PostModelT> entityModels)
		{
			return await TaskHelper.RunConcurrentTasksAsync(entityModels, 
				entityModel => ConvertEntityModelAsync(entityModel));
		}
		/// <summary>
		/// Try to map the given <paramref name="entity"/> to <typeparamref name="DestinationT"/>. If the entity
		/// has parent or children entities, they will be mapped to their respective IDs on the <typeparamref name="DestinationT"/>.
		/// </summary>
		/// <typeparam name="DestinationT"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public abstract Task<DestinationT> ConvertEntityAsync<DestinationT>(EntityT entity);
		public  async Task<List<DestinationT>> ConvertEntitiesAsync<DestinationT>(List<EntityT> entities)
		{
			return await TaskHelper.RunConcurrentTasksAsync(entities,
				entity => ConvertEntityAsync<DestinationT>(entity));
		}
	}
}
