using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration
{
	public class LogEntityPageCommand<TGetModel, TInclusionConfiguration> : IRequest
		where TGetModel : GetModelDto
	{
		public LogEntityPageCommand(PaginatedList<TGetModel> entityPage,
			IPrintOptions printOptions,
			TInclusionConfiguration inclusionConfig)
		{
			EntityPage = entityPage;
			PrintOptions = printOptions;
			InclusionConfig = inclusionConfig;
		}

		public PaginatedList<TGetModel> EntityPage { get; }
		public IPrintOptions PrintOptions { get; }
		public TInclusionConfiguration InclusionConfig { get; }
	}

	public class LogEntityPageCommandHandler<TRequest, TGetModel, TInclusionConfiguration> :
		IRequestHandler<TRequest>
		where TRequest : LogEntityPageCommand<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		private readonly IConfigurableEntityLogger<TGetModel, TInclusionConfiguration> _configEntityLogger;

		public LogEntityPageCommandHandler(
			IConfigurableEntityLogger<TGetModel, TInclusionConfiguration> configEntityLogger)
		{
			_configEntityLogger = configEntityLogger;
		}

		public Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
		{
			_configEntityLogger.Configure(request.InclusionConfig);

			if (request.PrintOptions.Print)
				_configEntityLogger.LogEntityPage(request.EntityPage);

			return Unit.Task;
		}
	}
}
