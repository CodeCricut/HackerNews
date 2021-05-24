using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.LogEntities;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.LogEntityPage
{
	public class LogBoardPageCommand : LogEntityPageCommand<GetBoardModel>
	{
		public LogBoardPageCommand(PaginatedList<GetBoardModel> entities, 
			IPrintOptions printOptions) : base(entities, printOptions)
		{
		}
	}

	public class LogBoardPageCommandHandler : LogEntityPageCommandHandler<LogBoardPageCommand, GetBoardModel>,
	{
		public LogBoardPageCommandHandler(IEntityLogger<GetBoardModel> entityLogger) : base(entityLogger)
		{
		}
	}
}
