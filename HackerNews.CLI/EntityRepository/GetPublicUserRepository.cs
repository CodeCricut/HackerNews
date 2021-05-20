using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class GetPublicUserRepository : IGetEntityRepository<GetPublicUserModel>
	{
		private readonly IEntityApiClient<RegisterUserModel, GetPublicUserModel> _entityApiClient;
		private readonly ILogger<GetPublicUserRepository> _logger;

		public GetPublicUserRepository(IEntityApiClient<RegisterUserModel, GetPublicUserModel> entityApiClient,
			ILogger<GetPublicUserRepository> logger)
		{
			_entityApiClient = entityApiClient;
			_logger = logger;
		}

		public Task<GetPublicUserModel> GetByIdAsync(int id)
		{
			_logger.LogTrace("Getting user with ID  " + id);

			return _entityApiClient.GetByIdAsync(id);
		}

		public async Task<PaginatedList<GetPublicUserModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			_logger.LogTrace("Getting users with IDs " + ids.ToDelimitedList(','));

			var users = await  _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);

			if (users.TotalCount != ids.Count()) _logger.LogWarning($"Expected {ids.Count()} articles, got {users.TotalCount}.");

			return users;
		}

		public Task<PaginatedList<GetPublicUserModel>> GetPageAsync(PagingParams pagingParams)
		{
			_logger.LogTrace("Getting comment page");

			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
