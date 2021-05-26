using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.LogJwt
{
	public class LogJwtCommand : IRequest
	{
		public LogJwtCommand(Jwt jwt)
		{
			Jwt = jwt;
		}

		public Jwt Jwt { get; }
	}

	public class LogJwtCommandHandler : IRequestHandler<LogJwtCommand>
	{
		private readonly IJwtLogger _jwtLogger;

		public LogJwtCommandHandler(IJwtLogger jwtLogger)
		{
			_jwtLogger = jwtLogger;
		}

		public Task<Unit> Handle(LogJwtCommand request, CancellationToken cancellationToken)
		{
			_jwtLogger.LogJwt(request.Jwt);

			return Unit.Task;
		}
	}
}
