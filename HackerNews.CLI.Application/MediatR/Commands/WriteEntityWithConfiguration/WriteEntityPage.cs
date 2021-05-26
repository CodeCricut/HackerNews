using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration
{
	public class WriteEntityCommand<TGetModel, TInclusionConfiguration> : IRequest
		where TGetModel : GetModelDto
	{
		public WriteEntityCommand(TGetModel entity, 
			IFileOptions options, 
			TInclusionConfiguration inclusionConfig)
		{
			Entity = entity;
			Options = options;
			InclusionConfig = inclusionConfig;
		}

		public TGetModel Entity { get; }
		public IFileOptions Options { get; }
		public TInclusionConfiguration InclusionConfig { get; }
	}

	public class WriteEntityCommandHandler<TRequest, TGetModel, TInclusionConfiguration> :
		IRequestHandler<TRequest>
		where TRequest : WriteEntityCommand<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		private readonly IConfigurableEntityWriter<TGetModel, TInclusionConfiguration> _configEntityWriter;

		public WriteEntityCommandHandler(IConfigurableEntityWriter<TGetModel, TInclusionConfiguration> configEntityWriter)
		{
			_configEntityWriter = configEntityWriter;
		}

		public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
		{
			_configEntityWriter.Configure(request.InclusionConfig);

			if (!string.IsNullOrEmpty(request.Options.FileLocation))
				await _configEntityWriter.WriteEntityAsync(request.Options.FileLocation, request.Entity);

			return Unit.Value;
		}
	}
}
