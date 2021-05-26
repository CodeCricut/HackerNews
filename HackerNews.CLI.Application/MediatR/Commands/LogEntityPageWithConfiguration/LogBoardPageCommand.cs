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

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration
{
	public class LogBoardPageCommand : LogEntityPageCommand<GetBoardModel, BoardInclusionConfiguration>
	{
		public LogBoardPageCommand(PaginatedList<GetBoardModel> entityPage, IPrintOptions printOptions, BoardInclusionConfiguration inclusionConfig) : base(entityPage, printOptions, inclusionConfig)
		{
		}
	}

	public class LogBoardPageCommandHandler :
		LogEntityPageCommandHandler<LogBoardPageCommand, GetBoardModel, BoardInclusionConfiguration>
	{
		public LogBoardPageCommandHandler(IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
