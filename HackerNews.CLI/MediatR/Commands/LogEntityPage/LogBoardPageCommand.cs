using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.LogEntities;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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

	public class LogBoardPageCommandHandler : LogEntityPageCommandHandler<GetBoardModel>,
		IRequestHandler<LogBoardPageCommand>
	{
		public LogBoardPageCommandHandler(IEntityLogger<GetBoardModel> entityLogger) : base(entityLogger)
		{
		}

		public Task<Unit> Handle(LogBoardPageCommand request, CancellationToken cancellationToken)
		{
			base.PrintEntities(request);

			return Unit.Task;
		}
	}
}
