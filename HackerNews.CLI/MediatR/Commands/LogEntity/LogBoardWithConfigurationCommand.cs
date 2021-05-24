using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.PrintEntity
{

	public class LogBoardWithConfigurationCommand : LogEntityCommand<GetBoardModel>
	{
		public LogBoardWithConfigurationCommand(GetBoardModel entity, 
			IPrintOptions printOptions,
			IBoardInclusionOptions inclusionOpts) : base(entity, printOptions)
		{
			InclusionOpts = inclusionOpts;
		}

		public IBoardInclusionOptions InclusionOpts { get; }
	}

	public class LogBoardWithConfigurationCommandHandler : LogEntityCommandHandler<LogBoardWithConfigurationCommand, GetBoardModel>
	{
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _entityLogger;

		public LogBoardWithConfigurationCommandHandler(IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> entityLogger) : base(entityLogger)
		{
			_entityLogger = entityLogger;
		}

		public override Task<Unit> Handle(LogBoardWithConfigurationCommand request, CancellationToken cancellationToken)
		{
			_entityLogger.Configure(request.InclusionOpts.ToInclusionConfiguration());

			return base.Handle(request, cancellationToken);
		}
	}
}
