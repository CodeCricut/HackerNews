using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration
{
	public class WriteEntityPageCommand<TGetModel, TInclusionConfiguration> : IRequest
		where TGetModel : GetModelDto
	{
		public WriteEntityPageCommand(PaginatedList<TGetModel> entityPage, 
			IFileOptions fileOptions,
			TInclusionConfiguration inclusionConfig)
		{
			EntityPage = entityPage;
			FileOptions = fileOptions;
			InclusionConfig = inclusionConfig;
		}

		public PaginatedList<TGetModel> EntityPage { get; }
		public IFileOptions FileOptions { get; }
		public TInclusionConfiguration InclusionConfig { get; }
	}

	public class WriteEntityPageCommandHandler<TRequest, TGetModel, TInclusionConfiguration> :
		IRequestHandler<TRequest>
		where TRequest : WriteEntityPageCommand<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		private readonly IConfigurableEntityWriter<TGetModel, TInclusionConfiguration> _configEntityWriter;

		public WriteEntityPageCommandHandler(IConfigurableEntityWriter<TGetModel, TInclusionConfiguration> configEntityWriter)
		{
			_configEntityWriter = configEntityWriter;
		}

		public virtual async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
		{
			_configEntityWriter.Configure(request.InclusionConfig);

			if (!string.IsNullOrEmpty(request.FileOptions.FileLocation))
				await _configEntityWriter.WriteEntityPageAsync(request.FileOptions.FileLocation, request.EntityPage);

			return Unit.Value;
		}
	}
}
