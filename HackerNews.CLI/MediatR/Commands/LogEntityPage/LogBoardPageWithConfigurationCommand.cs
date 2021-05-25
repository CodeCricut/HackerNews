using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.LogEntities;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.LogEntityPage
{
	public class LogBoardPageWithConfigurationCommand : LogEntityPageCommand<GetBoardModel>
	{
		public LogBoardPageWithConfigurationCommand(PaginatedList<GetBoardModel> entities,
			IPrintOptions printOptions,
			IBoardInclusionOptions inclusionOptions) : base(entities, printOptions)
		{
			InclusionOptions = inclusionOptions;
		}

		public IBoardInclusionOptions InclusionOptions { get; }
	}

	public class LogBoardPageWithConfigurationCommandHandler :
		LogEntityPageCommandHandler<LogBoardPageWithConfigurationCommand, GetBoardModel>
	{
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _entityLogger;

		public LogBoardPageWithConfigurationCommandHandler(IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> entityLogger)
			: base(entityLogger)
		{
			_entityLogger = entityLogger;
		}

		public override Task<Unit> Handle(LogBoardPageWithConfigurationCommand request, CancellationToken cancellationToken)
		{
			var inclusionConfig = request.InclusionOptions.ToInclusionConfiguration();
			_entityLogger.Configure(inclusionConfig);

			return base.Handle(request, cancellationToken);
		}
	}
}
