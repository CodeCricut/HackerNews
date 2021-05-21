using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Loggers
{
	public class ConfigurableArticleLogger 
		: IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration>
	{
		private readonly ILogger<ConfigurableArticleLogger> _logger;
		private readonly IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel> _articleInclusionReader;
		private ArticleInclusionConfiguration _inclusionConfig;

		public ConfigurableArticleLogger(ILogger<ConfigurableArticleLogger> logger,
			IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel> articleInclusionReader)
		{
			_logger = logger;
			_articleInclusionReader = articleInclusionReader;
			_inclusionConfig = new ArticleInclusionConfiguration();

			_logger.LogTrace("Created " + this.GetType().Name);
		}

		public void Configure(ArticleInclusionConfiguration config)
		{
			_logger.LogTrace("Configuring " + this.GetType().Name);

			_inclusionConfig = config;
		}

		public void LogEntity(GetArticleModel article)
		{
			_logger.LogDebug("Logging article.");

			LogArticle(article);
		}

		// TODO: this logging logic could be abstracted away as it is virtually the same for each entity.
		public void LogEntityPage(PaginatedList<GetArticleModel> articlePage)
		{
			_logger.LogDebug("Logging article page.");

			_logger.LogInformation($"ARTICLE PAGE {articlePage.PageIndex}/{articlePage.TotalPages}; Showing {articlePage.PageSize} / {articlePage.TotalCount} Articles");
			foreach (var article in articlePage.Items)
			{
				_logger.LogTrace($"Logging article with ID={article.Id} in article page.");
				LogArticle(article);
			}
		}

		private void LogArticle(GetArticleModel article)
		{
			Dictionary<string, string> articleDict = _articleInclusionReader.ReadIncludedKeyValues(_inclusionConfig, article);

			_logger.LogInformation("---------------------");
			foreach (var kvp in articleDict)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}
	}
}
