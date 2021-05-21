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
		: ConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration>
	{
		public ConfigurableArticleLogger(ILogger<ConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration>> logger, IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel> articleInclusionReader, ArticleInclusionConfiguration inclusionConfig) : base(logger, articleInclusionReader, inclusionConfig)
		{
		}

		protected override string GetEntityName()
			=> "Article";

		protected override string GetEntityNamePlural()
			=> "Articles";
	}
}
