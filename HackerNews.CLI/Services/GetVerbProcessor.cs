using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.ProgramServices;
using HackerNews.Domain.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.Services
{
	public interface IGetVerbProcessor<TPostModel, TGetModel>
	{
		Task ProcessGetVerbOptionsAsync(GetVerbOptions options);
	}

	public class GetVerbProcessor<TPostModel, TGetModel>
	{
		private readonly IEntityApiClient<TPostModel, TGetModel> _entityApiClient;
		private readonly IEntityLogger<TGetModel> _entityLogger;

		public GetVerbProcessor(IEntityApiClient<TPostModel, TGetModel> entityApiClient,
			IEntityLogger<TGetModel> entityLogger)
		{
			_entityApiClient = entityApiClient;
			_entityLogger = entityLogger;
		}

		public async Task ProcessGetVerbOptionsAsync(GetVerbOptions options)
		{
			if (options.Id > 0)
			{
				TGetModel entity = await GetEntityAsync(options.Id);
				OutputEntity(entity);
				return;
			}

			PagingParams pagingParams = new PagingParams();
			if (options.PageNumber > 0) pagingParams.PageNumber = options.PageNumber;
			if (options.PageSize > 0) pagingParams.PageSize = options.PageSize;

			PaginatedList<TGetModel> entityPage;
			if (options.Ids.Count() > 0)
				 entityPage = await GetEntitiesAsync(options.Ids, pagingParams);
			else
				 entityPage = await GetEntitiesAsync(pagingParams);

			OutputEntityPage(entityPage);
		}

		protected virtual Task<PaginatedList<TGetModel>> GetEntitiesAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		protected virtual Task<PaginatedList<TGetModel>> GetEntitiesAsync(PagingParams pagingParams)
		{
			return _entityApiClient.GetPageAsync(pagingParams);
		}

		protected virtual Task<TGetModel> GetEntityAsync(int id)
		{
			return _entityApiClient.GetByIdAsync(id);
		}

		protected virtual void OutputEntityPage(PaginatedList<TGetModel> entityPage)
		{
			_entityLogger.LogEntityPage(entityPage);
		}

		protected virtual void OutputEntity(TGetModel entity)
		{
			_entityLogger.LogEntity(entity);
		}
	}
}
