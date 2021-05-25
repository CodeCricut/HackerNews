using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class CommentFinder : IEntityFinder<GetCommentModel>
	{
		private readonly IEntityApiClient<PostCommentModel, GetCommentModel> _entityApiClient;
		private readonly ILogger<CommentFinder> _logger;

		public CommentFinder(IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient,
			ILogger<CommentFinder> logger)
		{
			_entityApiClient = entityApiClient;
			_logger = logger;
		}

		public Task<GetCommentModel> GetByIdAsync(int id)
		{
			_logger.LogTrace("Getting comment with ID  " + id);

			return _entityApiClient.GetByIdAsync(id);
		}

		public async Task<PaginatedList<GetCommentModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			_logger.LogTrace("Getting comments with IDs " + ids.ToDelimitedList(','));

			var commentPage = await _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);

			if (commentPage.TotalCount != ids.Count()) _logger.LogWarning($"Expected {ids.Count()} articles, got {commentPage.TotalCount}.");

			return commentPage;
		}

		public Task<PaginatedList<GetCommentModel>> GetPageAsync(PagingParams pagingParams)
		{
			_logger.LogTrace("Getting comment page");

			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
