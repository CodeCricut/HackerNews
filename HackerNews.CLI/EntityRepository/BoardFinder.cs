using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class BoardFinder : IEntityFinder<GetBoardModel>
	{
		private readonly IEntityApiClient<PostBoardModel, GetBoardModel> _entityApiClient;
		private readonly ILogger<BoardFinder> _logger;

		public BoardFinder(IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient,
			ILogger<BoardFinder> logger)
		{
			_entityApiClient = entityApiClient;
			_logger = logger;

			logger.LogTrace("Created " + this.GetType().Name);
		}

		public async Task<GetBoardModel> GetByIdAsync(int id)
		{
			_logger.LogTrace("Getting board with ID " + id);

			GetBoardModel board = await _entityApiClient.GetByIdAsync(id);

			if (board == null) _logger.LogWarning("Could not find board with ID " + id);

			return board;
		}

		public async Task<PaginatedList<GetBoardModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			_logger.LogTrace("Getting boards with IDs " + ids.ToDelimitedList(','));

			var boards = await _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);

			if (boards.TotalCount != ids.Count()) _logger.LogWarning($"Expected {ids.Count()} boards, got {boards.TotalCount}.");

			return boards;
		}

		public Task<PaginatedList<GetBoardModel>> GetPageAsync(PagingParams pagingParams)
		{
			_logger.LogTrace("Getting boards page");

			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
