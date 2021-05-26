using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration
{

	public class LogBoardCommand : LogEntityCommand<GetBoardModel, BoardInclusionConfiguration>
	{
		public LogBoardCommand(GetBoardModel entity, IPrintOptions options, BoardInclusionConfiguration inclusionConfig) : base(entity, options, inclusionConfig)
		{
		}
	}

	public class LogBoardCommandHandler : LogEntityCommandHandler<LogBoardCommand, GetBoardModel, BoardInclusionConfiguration>
	{
		public LogBoardCommandHandler(IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
