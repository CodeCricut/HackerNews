using CleanEntityArchitecture.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiReader<TGetModel>
		where TGetModel : GetModelDto
	{
		Task<PagedListResponse<TGetModel>> GetEndpointAsync(string endpoint, PagingParams pagingParams);
		Task<TGetModel> GetEndpointAsync(string endpoint, int id);
	}
}
