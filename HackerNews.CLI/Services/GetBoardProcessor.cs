using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.Services
{
	public class GetBoardProcessor : GetVerbProcessor<GetBoardModel>
	{
		private readonly IBoardApiClient _boardApiClient;
		private readonly IEntityLogger<GetBoardModel> _boardLogger;

		public GetBoardProcessor(IBoardApiClient boardApiClient,
			IEntityLogger<GetBoardModel> boardLogger)
		{
			_boardApiClient = boardApiClient;
			_boardLogger = boardLogger;
		}

		protected override Task<PaginatedList<GetBoardModel>> GetEntitiesAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _boardApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		protected override Task<PaginatedList<GetBoardModel>> GetEntitiesAsync(PagingParams pagingParams)
		{
			return _boardApiClient.GetPageAsync(pagingParams);
		}

		protected override Task<GetBoardModel> GetEntityAsync(int id)
		{
			return _boardApiClient.GetByIdAsync(id);
		}

		protected override void OutputEntityPage(PaginatedList<GetBoardModel> entityPage)
		{
			_boardLogger.LogEntityPage(entityPage);
		}

		protected override void OutputEntity(GetBoardModel entity)
		{
			_boardLogger.LogEntity(entity);
		}
	}
}
