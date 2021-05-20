using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HackerNews.CLI.Loggers
{
	public class ArticleLogger : IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration>
	{
		private readonly ILogger<ArticleLogger> _logger;
		private readonly IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel> _articleInclusionReader;
		private ArticleInclusionConfiguration _inclusionConfig;

		public ArticleLogger(ILogger<ArticleLogger> logger,
			IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel> articleInclusionReader)
		{
			_logger = logger;
			_articleInclusionReader = articleInclusionReader;
			_inclusionConfig = new ArticleInclusionConfiguration();
		}

		public void Configure(ArticleInclusionConfiguration config)
		{
			_inclusionConfig = config;
		}

		public void LogEntity(GetArticleModel article)
		{
			LogArticle(article);
		}

		// TODO: this logging logic could be abstracted away as it is virtually the same for each entity.
		public void LogEntityPage(PaginatedList<GetArticleModel> articlePage)
		{
			_logger.LogInformation($"ARTICLE PAGE {articlePage.PageIndex}/{articlePage.TotalPages}; Showing {articlePage.PageSize} / {articlePage.TotalCount} Articles");
			foreach (var article in articlePage.Items)
				LogArticle(article);
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
