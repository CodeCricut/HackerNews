using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.Domain.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Helpers
{
	interface IApiConsumer<TEntity, TPostEntityModel, TGetEntityModel>
		where TEntity : DomainEntity
		where TPostEntityModel : PostEntityModel
		where TGetEntityModel : GetEntityModel
	{
		Task<TGetEntityModel> PostEndpointAsync(string endpoint, TPostEntityModel postModel);
		Task<object> PostEndpointAsync(string endpoint, IEnumerable<TPostEntityModel> postModels);
		Task<IEnumerable<TGetEntityModel>> GetEndpointAsync(string endpoint, PagingParams pagingParams);
		Task<TGetEntityModel> GetEndpointAsync(string endpoint, int id);
		Task<object> PutEndpointAsync(string endpoint, TPostEntityModel updateModel);
		Task<object> DeleteEndpointAsync(string endpoint, int id);
	}
}
