using AutoMapper;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters
{
	public abstract class EntityConverter<EntityT, PostModelT, GetModelT> : IEntityConverter<EntityT, PostModelT, GetModelT> 
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
		/// Try to map the given <typeparamref name="PostModelT"/> to the <typeparamref name="EntityT"/>. If there is not profile given for 
		/// the mapping behavior (which is how AutoMapper knows how to map things), then an <see cref="AutoMapper.AutoMapperMappingException"/> 
		/// will be thrown.
		/// </summary>
		/// <param name="entityModel"></param>
		/// <returns></returns>
		public abstract Task<EntityT> ConvertEntityModelAsync(PostModelT entityModel);

		/// <summary>
		/// Try to map the each member in <paramref name="entityModels"/> to the destination in the list of <typeparamref name="EntityT"/>. If there is not profile given for 
		/// the mapping behavior (which is how AutoMapper knows how to map things), then an <see cref="AutoMapper.AutoMapperMappingException"/> 
		/// will be thrown.
		/// </summary>
		/// <param name="entityModel"></param>
		/// <returns></returns>
		public async Task<List<EntityT>> ConvertEntityModelsAsync(List<PostModelT> entityModels)
		{
			return await TaskHelper.RunConcurrentTasksAsync(entityModels,
				entityModel => ConvertEntityModelAsync(entityModel));
		}

		/// <summary>
		/// Try to map the given <typeparamref name="EntityT"/> to the <typeparamref name="DestinationT"/>. If there is not profile given for 
		/// the mapping behavior (which is how AutoMapper knows how to map things), then an <see cref="AutoMapper.AutoMapperMappingException"/> 
		/// will be thrown.
		/// </summary>
		/// <param name="entityModel"></param>
		/// <returns></returns>
		public abstract Task<GetModelT> ConvertEntityAsync<GetModelT>(EntityT entity);

		/// <summary>
		/// Try to map the each member in <paramref name="entities"/> to the destination in the list of <typeparamref name="DestinationT"/>. If there is not profile given for 
		/// the mapping behavior (which is how AutoMapper knows how to map things), then an <see cref="AutoMapper.AutoMapperMappingException"/> 
		/// will be thrown.
		/// </summary>
		/// <param name="entityModel"></param>
		/// <returns></returns>
		public async Task<List<GetModelT>> ConvertEntitiesAsync<GetModelT>(List<EntityT> entities)
		{
			return await TaskHelper.RunConcurrentTasksAsync(entities,
				entity => ConvertEntityAsync<GetModelT>(entity));
		}
	}
}
