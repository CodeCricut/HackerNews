using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.LogEntity
{
	public class LogBoardCommand : LogEntityCommand<GetBoardModel>, IRequest
	{
		public LogBoardCommand(GetBoardModel entity, IPrintOptions options) : base(entity, options)
		{
		}
	}

	public class LogBoardCommandHandler : LogEntityCommandHandler<GetBoardModel>, 
		IRequestHandler<LogBoardCommand>
	{
		public LogBoardCommandHandler(IEntityLogger<GetBoardModel> entityLogger) : base(entityLogger)
		{
		}

		public async Task<Unit> Handle(LogBoardCommand request, CancellationToken cancellationToken)
		{
			await base.PrintEntity(request);

			return Unit.Value;
		}
	}
}
