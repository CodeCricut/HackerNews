using CleanEntityArchitecture.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiReader
	{
		Task<PagedListResponse<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, PagingParams pagingParams) 
			where TGetModel : GetModelDto, new();

		Task<PagedListResponse<TGetModel>> GetEndpointWithQueryAsync<TGetModel>(string endpoint, string query, PagingParams pagingParams) 
			where TGetModel : GetModelDto, new();

		Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint, int id) where TGetModel : GetModelDto, new();
		Task<IEnumerable<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, IEnumerable<int> ids) where TGetModel : GetModelDto, new();
		Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint) where TGetModel : GetModelDto, new();
	}
}
