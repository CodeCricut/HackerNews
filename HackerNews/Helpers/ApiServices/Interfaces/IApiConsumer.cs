using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Helpers
{
	interface IApiConsumer<TEntity, TPostEntityModel, TGetPublicEntityModel, TGetPrivateEntityModel> 
		where TEntity : DomainEntity
		where TPostEntityModel : PostEntityModel
		where TGetPublicEntityModel : GetEntityModel
		where TGetPrivateEntityModel : GetEntityModel
	{
		Task<TGetEntityModel> PostEndpointAsync(string endpoint, TPostEntityModel postModel);
		Task<object> PostEndpointAsync(string endpoint, IEnumerable<TPostEntityModel> postModels);
		Task<IEnumerable<TGetEntityModel>> GetEndpointAsync(string endpoint, PagingParams pagingParams);
		Task<TGetEntityModel> GetEndpointAsync(string endpoint, int id);
		Task<object> PutEndpointAsync(string endpoint, TPostEntityModel updateModel);
		Task<object> DeleteEndpointAsync(string endpoint, int id);
	}
}
