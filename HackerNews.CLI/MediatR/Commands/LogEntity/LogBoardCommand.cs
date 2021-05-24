using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.MediatR.Commands.LogEntity
{
	public class LogBoardCommand : LogEntityCommand<GetBoardModel>
	{
		public LogBoardCommand(GetBoardModel entity, IPrintOptions options) : base(entity, options)
		{
		}
	}

	public class LogBoardCommandHandler : LogEntityCommandHandler<LogBoardCommand, GetBoardModel>
	{
		public LogBoardCommandHandler(IEntityLogger<GetBoardModel> entityLogger) : base(entityLogger)
		{
		}
	}
}
