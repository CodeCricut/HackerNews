using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HackerNews.CLI.Loggers
{
	public abstract class EntityLogger<TGetModel> : IEntityLogger<TGetModel>
	{
		private readonly ILogger<EntityLogger<TGetModel>> _logger;
		private readonly IEntityReader<TGetModel> _entityReader;

		public EntityLogger(ILogger<EntityLogger<TGetModel>> logger,
			IEntityReader<TGetModel> entityReader)
		{
			_logger = logger;
			_entityReader = entityReader;
		}

		public void LogEntity(TGetModel entity)
		{
			_logger.LogTrace($"Logging {GetEntityName()}.");

			LogEntityInstance(entity);
		}

		public void LogEntityPage(PaginatedList<TGetModel> entityPage)
		{
			_logger.LogTrace($"Logging {GetEntityName()} page.");

			_logger.LogInformation($"{GetEntityName().ToUpper()} PAGE {entityPage.PageIndex}/{entityPage.TotalPages}; Showing {entityPage.PageSize} / {entityPage.TotalCount} {GetEntityPlural()}");
			foreach (var entity in entityPage.Items)
			{
				LogEntityInstance(entity);
			}
		}

		private void LogEntityInstance(TGetModel entity)
		{
			Dictionary<string, string> entityPropDictionary = _entityReader.ReadAllKeyValues(entity);

			_logger.LogInformation("---------------------");
			foreach (var kvp in entityPropDictionary)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}

		protected abstract string GetEntityName();
		protected abstract string GetEntityPlural();
	}
}
