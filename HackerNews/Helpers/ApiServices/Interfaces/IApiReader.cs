using HackerNews.Domain.Models;
using HackerNews.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiReader<TGetModel>
		where TGetModel : GetEntityModel
	{
		Task<IEnumerable<TGetModel>> GetEndpointAsync(string endpoint, PagingParams pagingParams);
		Task<TGetModel> GetEndpointAsync(string endpoint, int id);
	}
}
