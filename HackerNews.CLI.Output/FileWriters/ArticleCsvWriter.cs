using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Output.FileWriters;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class ArticleCsvWriter : EntityCsvWriter<GetArticleModel, ArticleInclusionConfiguration>
	{
		public ArticleCsvWriter(ICsvFileWriter csvWriter,
			ILogger<EntityCsvWriter<GetArticleModel, ArticleInclusionConfiguration>> logger,
			IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel> inclusionReader,
			ArticleInclusionConfiguration inclusionConfig) : base(csvWriter, logger, inclusionReader, inclusionConfig)
		{
		}
	}
}
