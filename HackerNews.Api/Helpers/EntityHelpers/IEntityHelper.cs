using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	// model and entity methods could be refactored into separate interfaces/classes
	public interface IEntityHelper<EntityT, PostModelT, GetModelT>
	{
		//
		Task<GetModelT> PostEntityModelAsync(PostModelT entityModel);
		//
		Task PostEntityModelsAsync(List<PostModelT> entityModels);

		//
		Task<GetModelT> GetEntityModelAsync(int id);
		//
		Task<List<GetModelT>> GetAllEntityModelsAsync();

		//
		Task SoftDeleteEntityAsync(int id);

		//
		Task<GetModelT> PutEntityModelAsync(int id, PostModelT entityModel);

		//Task<EntityT> GetEntityAsync(int id);
		//Task<List<EntityT>> GetAllEntitiesAsync();

		// void UpdateEntityProperties(EntityT entity, EntityT newEntity);
	}
}
