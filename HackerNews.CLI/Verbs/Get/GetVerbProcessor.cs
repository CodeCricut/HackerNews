using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	public interface IGetVerbProcessor<TPostModel, TGetModel>
	{
		Task ProcessGetVerbOptionsAsync(GetVerbOptions options);
	}

	public abstract class GetVerbProcessor<TPostModel, TGetModel> : IGetVerbProcessor<TPostModel, TGetModel>
	{
		private readonly IEntityApiClient<TPostModel, TGetModel> _entityApiClient;
		private readonly IEntityLogger<TGetModel> _entityLogger;
		private readonly IEntityWriter<TGetModel> _entityWriter;

		public GetVerbProcessor(IEntityApiClient<TPostModel, TGetModel> entityApiClient,
			IEntityLogger<TGetModel> entityLogger,
			IEntityWriter<TGetModel> entityWriter)
		{
			_entityApiClient = entityApiClient;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
		}

		public async Task ProcessGetVerbOptionsAsync(GetVerbOptions options)
		{
			ConfigureWriter(options, _entityWriter);

			if (options.Id > 0)
			{
				TGetModel entity = await GetEntityAsync(options.Id);
				OutputEntity(options, entity);
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

			OutputEntityPage(options, entityPage);
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

		protected virtual void OutputEntityPage(GetVerbOptions options, PaginatedList<TGetModel> entityPage)
		{
			if (options.Print)
				_entityLogger.LogEntityPage(entityPage);
			if (!string.IsNullOrEmpty(options.File))
				_entityWriter.WriteEntityPageAsync(options.File, entityPage);
		}

		protected virtual void OutputEntity(GetVerbOptions options, TGetModel entity)
		{
			if (options.Print)
				_entityLogger.LogEntity(entity);
			if (!string.IsNullOrEmpty(options.File))
				_entityWriter.WriteEntityAsync(options.File, entity);
		}

		protected abstract void ConfigureWriter(GetVerbOptions options, IEntityWriter<TGetModel> writer);
	}
}
