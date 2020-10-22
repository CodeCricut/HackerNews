using CleanEntityArchitecture.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiReader
	{
		Task<PagedListResponse<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, PagingParams pagingParams, bool includeDeleted = false)
			where TGetModel : GetModelDto, new();

		Task<PagedListResponse<TGetModel>> GetEndpointWithQueryAsync<TGetModel>(string endpoint, string query, PagingParams pagingParams, bool includeDeleted = false)
			where TGetModel : GetModelDto, new();

		Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint, int id, bool includeDeleted = false) where TGetModel : GetModelDto, new();

		Task<IEnumerable<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, IEnumerable<int> ids, bool includeDeleted = false) where TGetModel : GetModelDto, new();
		Task<PagedListResponse<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, IEnumerable<int> ids, PagingParams pagingParams, bool includeDeleted = false) where TGetModel : GetModelDto, new();

		Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint, bool includeDeleted = false) where TGetModel : GetModelDto, new();
		Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint, string jwt, bool includeDeleted = false) where TGetModel : GetModelDto, new();
	}
}
