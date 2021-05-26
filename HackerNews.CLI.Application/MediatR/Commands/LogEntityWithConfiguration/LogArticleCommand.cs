using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.Options;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Articles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration
{
	public class LogArticleCommand : LogEntityCommand<GetArticleModel, ArticleInclusionConfiguration>
	{
		public LogArticleCommand(GetArticleModel entity, IPrintOptions options, ArticleInclusionConfiguration inclusionConfig) : base(entity, options, inclusionConfig)
		{
		}
	}

	public class LogArticleCommandHandler :
		LogEntityCommandHandler<LogArticleCommand, GetArticleModel, ArticleInclusionConfiguration>
	{
		public LogArticleCommandHandler(IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
