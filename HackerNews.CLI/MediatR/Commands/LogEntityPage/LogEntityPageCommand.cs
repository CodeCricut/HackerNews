using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.LogEntities
{
	public class LogEntityPageCommand<TGetModel> : IRequest
		where TGetModel : GetModelDto
	{
		public LogEntityPageCommand(PaginatedList<TGetModel> entities, IPrintOptions printOptions)
		{
			EntityPage = entities;
			PrintOptions = printOptions;
		}

		public PaginatedList<TGetModel> EntityPage { get; }
		public IPrintOptions PrintOptions { get; }
	}

	public class LogEntityPageCommandHandler<TRequest, TGetModel> :
		IRequestHandler<TRequest>
		where TRequest : LogEntityPageCommand<TGetModel>
		where TGetModel : GetModelDto
	{
		private readonly IEntityLogger<TGetModel> _entityLogger;

		public LogEntityPageCommandHandler(IEntityLogger<TGetModel> entityLogger)
		{
			_entityLogger = entityLogger;
		}

		public Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
		{
			if (request.PrintOptions.Print)
				_entityLogger.LogEntityPage(request.EntityPage);

			return Unit.Task;
		}
	}
}
