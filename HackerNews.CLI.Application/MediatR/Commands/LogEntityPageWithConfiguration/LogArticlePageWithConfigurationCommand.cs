using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.LogEntities;
using HackerNews.CLI.Options;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPage
{
	public class LogArticlePageWithConfigurationCommand : LogEntityPageCommand<GetArticleModel, ArticleInclusionConfiguration>
	{
		public LogArticlePageWithConfigurationCommand(PaginatedList<GetArticleModel> entityPage, IPrintOptions printOptions, ArticleInclusionConfiguration inclusionConfig) : base(entityPage, printOptions, inclusionConfig)
		{
		}
	}

	public class LogArticlePageWithConfigurationCommandHandler
		: LogEntityPageCommandHandler<LogArticlePageWithConfigurationCommand, GetArticleModel, ArticleInclusionConfiguration>
	{
		public LogArticlePageWithConfigurationCommandHandler(IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
