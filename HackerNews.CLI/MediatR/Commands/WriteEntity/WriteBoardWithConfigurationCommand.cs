using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.WriteEntity
{
	public class WriteBoardWithConfigurationCommand : WriteEntityCommand<GetBoardModel>, IRequest
	{
		public WriteBoardWithConfigurationCommand(
			GetBoardModel entity, 
			IFileOptions options,
			IBoardInclusionOptions inclusionOptions) : base(entity, options)
		{
			InclusionOptions = inclusionOptions;
		}

		public IBoardInclusionOptions InclusionOptions { get; }
	}

	public class WriteBoardCommandWithConfigurationHandler : WriteEntityCommandHandler<GetBoardModel>, IRequestHandler<WriteBoardWithConfigurationCommand>
	{
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _entityWriter;

		public WriteBoardCommandWithConfigurationHandler(IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> entityWriter) : base(entityWriter)
		{
			_entityWriter = entityWriter;
		}

		public Task<Unit> Handle(WriteBoardWithConfigurationCommand request, CancellationToken cancellationToken)
		{
			_entityWriter.Configure(request.InclusionOptions.ToInclusionConfiguration());

			WriteEntity(request);

			return Unit.Task;
		}
	}
}
