using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.PrintEntity
{
	public class LogEntityCommand<TGetModel> : IRequest
		where TGetModel : GetModelDto
	{
		public LogEntityCommand(TGetModel entity, IPrintOptions options)
		{
			Entity = entity;
			Options = options;
		}

		public TGetModel Entity { get; }
		public IPrintOptions Options { get; }
	}


	public class LogEntityCommandHandler<TRequest, TGetModel> :
		IRequestHandler<TRequest>
		where TRequest : LogEntityCommand<TGetModel>
		where TGetModel : GetModelDto
	{
		private readonly IEntityLogger<TGetModel> _entityLogger;

		public LogEntityCommandHandler(IEntityLogger<TGetModel> entityLogger)
		{
			_entityLogger = entityLogger;
		}

		public virtual Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
		{
			// TODO: maybe extract this logic out somewhere so that you can use it without using a request
			if (request.Options.Print)
				_entityLogger.LogEntity(request.Entity);

			return Unit.Task;
		}
	}
}
