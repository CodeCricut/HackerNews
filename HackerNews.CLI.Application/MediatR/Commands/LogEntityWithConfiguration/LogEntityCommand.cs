using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration
{
	public class LogEntityCommand<TGetModel, TInclusionConfiguration> : IRequest
		where TGetModel : GetModelDto
	{
		public LogEntityCommand(TGetModel entity, 
			IPrintOptions options,
			TInclusionConfiguration inclusionConfig)
		{
			Entity = entity;
			PrintOptions = options;
			InclusionConfig = inclusionConfig;
		}

		public TGetModel Entity { get; }
		public IPrintOptions PrintOptions { get; }
		public TInclusionConfiguration InclusionConfig { get; }
	}


	public class LogEntityCommandHandler<TRequest, TGetModel, TInclusionConfiguration> :
		IRequestHandler<TRequest>
		where TRequest : LogEntityCommand<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		private readonly IConfigurableEntityLogger<TGetModel, TInclusionConfiguration> _configEntityLogger;

		public LogEntityCommandHandler(IConfigurableEntityLogger<TGetModel, TInclusionConfiguration> configEntityLogger)
		{
			_configEntityLogger = configEntityLogger;
		}

		public Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
		{
			_configEntityLogger.Configure(request.InclusionConfig);

			if (request.PrintOptions.Print)
				_configEntityLogger.LogEntity(request.Entity);

			return Unit.Task;
		}
	}
}
