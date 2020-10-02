using HackerNews.Domain;
using HackerNews.Domain.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	// model and entity methods could be refactored into separate interfaces/classes
	public interface IEntityService<EntityT, PostModelT, GetModelT>
	{
		//
		Task<GetModelT> PostEntityModelAsync(PostModelT entityModel, User currentUser);
		//
		Task PostEntityModelsAsync(List<PostModelT> entityModels, User currentUser);

		//
		Task<GetModelT> GetEntityModelAsync(int id);
		//
		Task<List<GetModelT>> GetAllEntityModelsAsync(PagingParams pagingParams);

		//
		Task SoftDeleteEntityAsync(int id, User currentUser);

		//
		Task<GetModelT> PutEntityModelAsync(int id, PostModelT entityModel, User currentUser);

		//Task<EntityT> GetEntityAsync(int id);
		//Task<List<EntityT>> GetAllEntitiesAsync();

		// void UpdateEntityProperties(EntityT entity, EntityT newEntity);
	}
}
