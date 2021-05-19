using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class GetPublicUserRepository : IGetEntityRepository<GetPublicUserModel>
	{
		private readonly IEntityApiClient<RegisterUserModel, GetPublicUserModel> _entityApiClient;

		public GetPublicUserRepository(IEntityApiClient<RegisterUserModel, GetPublicUserModel> entityApiClient)
		{
			_entityApiClient = entityApiClient;
		}

		public Task<GetPublicUserModel> GetByIdAsync(int id)
		{
			return _entityApiClient.GetByIdAsync(id);
		}

		public Task<PaginatedList<GetPublicUserModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		public Task<PaginatedList<GetPublicUserModel>> GetPageAsync(PagingParams pagingParams)
		{
			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
