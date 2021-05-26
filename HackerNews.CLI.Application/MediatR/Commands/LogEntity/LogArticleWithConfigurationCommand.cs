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

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntity
{
	public class LogArticleWithConfigurationCommand : LogEntityCommand<GetArticleModel>
	{
		public LogArticleWithConfigurationCommand(GetArticleModel entity, 
			IPrintOptions options,
			IArticleInclusionOptions inclusionOptions) : base(entity, options)
		{
			InclusionOptions = inclusionOptions;
		}

		public IArticleInclusionOptions InclusionOptions { get; }
	}

	public class LogArticleWithConfigurationCommandHandler :
		LogEntityCommandHandler<LogArticleWithConfigurationCommand, GetArticleModel>
	{
		private readonly IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> _entityLogger;

		public LogArticleWithConfigurationCommandHandler(
			IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> entityLogger) : base(entityLogger)
		{
			_entityLogger = entityLogger;
		}

		public override Task<Unit> Handle(LogArticleWithConfigurationCommand request, CancellationToken cancellationToken)
		{
			_entityLogger.Configure(request.InclusionOptions.ToInclusionConfiguration());

			return base.Handle(request, cancellationToken);
		}
	}
}
