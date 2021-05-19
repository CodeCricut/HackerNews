using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class GetBoardRepository : IGetEntityRepository<GetBoardModel>
	{
		private readonly IEntityApiClient<PostBoardModel, GetBoardModel> _entityApiClient;

		public GetBoardRepository(IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient)
		{
			_entityApiClient = entityApiClient;
		}

		public Task<GetBoardModel> GetByIdAsync(int id)
		{
			return _entityApiClient.GetByIdAsync(id);
		}

		public Task<PaginatedList<GetBoardModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		public Task<PaginatedList<GetBoardModel>> GetPageAsync(PagingParams pagingParams)
		{
			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
