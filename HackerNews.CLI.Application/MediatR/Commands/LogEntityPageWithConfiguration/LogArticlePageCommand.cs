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

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration
{
	public class LogArticlePageCommand : LogEntityPageCommand<GetArticleModel, ArticleInclusionConfiguration>
	{
		public LogArticlePageCommand(PaginatedList<GetArticleModel> entityPage, IPrintOptions printOptions, ArticleInclusionConfiguration inclusionConfig) : base(entityPage, printOptions, inclusionConfig)
		{
		}
	}

	public class LogArticlePageCommandHandler
		: LogEntityPageCommandHandler<LogArticlePageCommand, GetArticleModel, ArticleInclusionConfiguration>
	{
		public LogArticlePageCommandHandler(IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
