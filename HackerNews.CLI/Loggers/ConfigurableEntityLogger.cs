using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HackerNews.CLI.Loggers
{
	public abstract class ConfigurableEntityLogger<TGetModel, TEntityInclusionConfiguration> : IConfigurableEntityLogger<TGetModel, TEntityInclusionConfiguration>
	{
		private readonly ILogger<ConfigurableEntityLogger<TGetModel, TEntityInclusionConfiguration>> _logger;
		private readonly IEntityInclusionReader<TEntityInclusionConfiguration, TGetModel> _entityInclusionReader;
		private TEntityInclusionConfiguration _inclusionConfig;

		public ConfigurableEntityLogger(ILogger<ConfigurableEntityLogger<TGetModel, TEntityInclusionConfiguration>> logger,
			IEntityInclusionReader<TEntityInclusionConfiguration, TGetModel> articleInclusionReader,
			TEntityInclusionConfiguration inclusionConfig)
		{
			_logger = logger;
			_entityInclusionReader = articleInclusionReader;
			_inclusionConfig = inclusionConfig;

			_logger.LogTrace("Created " + this.GetType().Name);
		}

		public void Configure(TEntityInclusionConfiguration config)
		{
			_logger.LogTrace("Configuring " + this.GetType().Name);

			_inclusionConfig = config;
		}

		public void LogEntity(TGetModel entity)
		{
			_logger.LogDebug($"Logging {GetEntityName()}.");

			LogEntityInstance(entity);
		}

		public void LogEntityPage(PaginatedList<TGetModel> entityPage)
		{
			_logger.LogDebug($"Logging {GetEntityName()} page.");

			_logger.LogInformation($"{GetEntityName().ToUpper()} PAGE {entityPage.PageIndex}/{entityPage.TotalPages}; Showing {entityPage.PageSize} / {entityPage.TotalCount} {GetEntityNamePlural()}");
			foreach (var entity in entityPage.Items)
			{
				LogEntityInstance(entity);
			}
		}

		private void LogEntityInstance(TGetModel entity)
		{
			Dictionary<string, string> entityPropertyDictionary = _entityInclusionReader.ReadIncludedKeyValues(_inclusionConfig, entity);

			_logger.LogInformation("---------------------");
			foreach (var kvp in entityPropertyDictionary)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}

		protected abstract string GetEntityName();
		protected abstract string GetEntityNamePlural();
	}
}
