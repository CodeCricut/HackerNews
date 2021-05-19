using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class GetCommentRepository : IGetEntityRepository<GetCommentModel>
	{
		private readonly IEntityApiClient<PostCommentModel, GetCommentModel> _entityApiClient;

		public GetCommentRepository(IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient)
		{
			_entityApiClient = entityApiClient;
		}

		public Task<GetCommentModel> GetByIdAsync(int id)
		{
			return _entityApiClient.GetByIdAsync(id);
		}

		public Task<PaginatedList<GetCommentModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		public Task<PaginatedList<GetCommentModel>> GetPageAsync(PagingParams pagingParams)
		{
			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
