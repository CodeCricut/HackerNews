using HackerNews.CLI.Application.MediatR.Commands.LogEntityPage;
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
	public class LogBoardPageWithConfigurationCommand : LogEntityPageCommand<GetBoardModel, BoardInclusionConfiguration>
	{
		public LogBoardPageWithConfigurationCommand(PaginatedList<GetBoardModel> entityPage, IPrintOptions printOptions, BoardInclusionConfiguration inclusionConfig) : base(entityPage, printOptions, inclusionConfig)
		{
		}
	}

	public class LogBoardPageWithConfigurationCommandHandler :
		LogEntityPageCommandHandler<LogBoardPageWithConfigurationCommand, GetBoardModel, BoardInclusionConfiguration>
	{
		public LogBoardPageWithConfigurationCommandHandler(IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
