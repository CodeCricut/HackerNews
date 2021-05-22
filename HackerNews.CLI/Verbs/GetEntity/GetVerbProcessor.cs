using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IGetVerbProcessor<GetModel, TOptions>

		where TOptions : IGetEntityOptions
	{
		void ConfigureProcessor(TOptions options);
		Task ProcessGetVerbOptionsAsync(TOptions options);
	}

	public abstract class GetVerbProcessor<TGetModel, TOptions> :
		IGetVerbProcessor<TGetModel, TOptions>
		where TOptions : IGetEntityOptions
	{
		private readonly ILogger<GetVerbProcessor<TGetModel, TOptions>> _logger;
		private readonly IVerbositySetter _appConfigurator;

		protected IGetEntityRepository<TGetModel> EntityRepository { get; private set; }
		protected IEntityLogger<TGetModel> EntityLogger { get; private set; }
		protected IEntityWriter<TGetModel> EntityWriter { get; private set; }

		public GetVerbProcessor(
			IGetEntityRepository<TGetModel> entityRepository,
			IEntityLogger<TGetModel> entityLogger,
			IEntityWriter<TGetModel> entityWriter,
			ILogger<GetVerbProcessor<TGetModel, TOptions>> logger,
			IVerbositySetter appConfigurator)
		{
			EntityRepository = entityRepository;
			EntityLogger = entityLogger;
			EntityWriter = entityWriter;
			_logger = logger;
			_appConfigurator = appConfigurator;
			logger.LogTrace("Created " + this.GetType().Name);
		}

		public async Task ProcessGetVerbOptionsAsync(TOptions options)
		{
			_logger.LogDebug($"Processing Get Verb Options [Processor name: {this.GetType().Name}] [Type name: {options.GetType().Name}]");

			if (options.Verbose)
				_appConfigurator.SetVerbository(true);

			ConfigureProcessor(options);

			if (options.Id > 0)
			{
				_logger.LogDebug($"Id passed into Get Verb Processor.");
				TGetModel entity = await EntityRepository.GetByIdAsync(options.Id);
				OutputEntity(options, entity);
				return;
			}

			PagingParams pagingParams = new PagingParams();
			if (options.PageNumber > 0) pagingParams.PageNumber = options.PageNumber;
			if (options.PageSize > 0) pagingParams.PageSize = options.PageSize;

			PaginatedList<TGetModel> entityPage;
			if (options.Ids.Count() > 0)
			{
				_logger.LogDebug($"Multiple IDs passed into Get Verb Processor.");
				entityPage = await EntityRepository.GetByIdsAsync(options.Ids, pagingParams);
			}
			else
			{
				_logger.LogDebug("No IDs passed into Get Verb Processor.");
				entityPage = await EntityRepository.GetPageAsync(pagingParams);
			}

			OutputEntityPage(options, entityPage);
		}


		protected virtual void OutputEntityPage(TOptions options, PaginatedList<TGetModel> entityPage)
		{
			_logger.LogDebug($"Outputting entity page [PageIndex={entityPage.PageIndex}] [PageSize={entityPage.PageSize}]");
			if (options.Print)
			{
				_logger.LogDebug($"Print option true.");
				EntityLogger.LogEntityPage(entityPage);
			}
			if (!string.IsNullOrEmpty(options.FileLocation))
			{
				_logger.LogDebug($"File location passed: {options.FileLocation}");
				EntityWriter.WriteEntityPageAsync(options.FileLocation, entityPage);
			}
		}

		protected virtual void OutputEntity(TOptions options, TGetModel entity)
		{
			_logger.LogDebug($"Outputting entity");

			if (options.Print)
			{
				_logger.LogDebug($"Print option true.");
				EntityLogger.LogEntity(entity);
			}

			if (!string.IsNullOrEmpty(options.FileLocation))
			{
				_logger.LogDebug($"File location passed: {options.FileLocation}");
				EntityWriter.WriteEntityAsync(options.FileLocation, entity);
			}
		}

		public abstract void ConfigureProcessor(TOptions options);
	}
}
