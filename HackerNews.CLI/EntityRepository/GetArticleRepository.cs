using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class GetArticleRepository : IGetEntityRepository<GetArticleModel>
	{
		private readonly IEntityApiClient<PostArticleModel, GetArticleModel> _entityApiClient;

		public GetArticleRepository(IEntityApiClient<PostArticleModel, GetArticleModel> entityApiClient)
		{
			_entityApiClient = entityApiClient;
		}

		public Task<GetArticleModel> GetByIdAsync(int id)
		{
			return _entityApiClient.GetByIdAsync(id);
		}

		public Task<PaginatedList<GetArticleModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		public Task<PaginatedList<GetArticleModel>> GetPageAsync(PagingParams pagingParams)
		{
			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
