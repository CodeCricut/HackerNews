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
	public class LogArticlePageWithConfigurationCommand : LogEntityPageCommand<GetArticleModel>
	{
		public LogArticlePageWithConfigurationCommand(
			PaginatedList<GetArticleModel> entities, 
			IPrintOptions printOptions,
			IArticleInclusionOptions inclusionOptions) : base(entities, printOptions)
		{
			InclusionOptions = inclusionOptions;
		}

		public IArticleInclusionOptions InclusionOptions { get; }
	}

	public class LogArticlePageWithConfigurationCommandHandler : LogEntityPageCommandHandler<LogArticlePageWithConfigurationCommand, GetArticleModel>
	{
		private readonly IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> _entityLogger;

		public LogArticlePageWithConfigurationCommandHandler(IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> entityLogger) : base(entityLogger)
		{
			_entityLogger = entityLogger;
		}

		public override Task<Unit> Handle(LogArticlePageWithConfigurationCommand request, CancellationToken cancellationToken)
		{
			_entityLogger.Configure(request.InclusionOptions.ToInclusionConfiguration());
			return base.Handle(request, cancellationToken);
		}
	}
}
