using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.WriteEntity
{
	public class WriteEntityCommand<TGetModel> : IRequest
		where TGetModel : GetModelDto
	{
		public WriteEntityCommand(TGetModel entity, IFileOptions options)
		{
			Entity = entity;
			Options = options;
		}

		public TGetModel Entity { get; }
		public IFileOptions Options { get; }
	}

	public class WriteEntityCommandHandler<TRequest, TGetModel> :
		IRequestHandler<TRequest>
		where TRequest : WriteEntityCommand<TGetModel>
		where TGetModel : GetModelDto
	{
		private readonly IEntityWriter<TGetModel> _entityWriter;

		public WriteEntityCommandHandler(IEntityWriter<TGetModel> entityWriter)
		{
			_entityWriter = entityWriter;
		}

		public virtual async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
		{
			if (!string.IsNullOrEmpty(request.Options.FileLocation))
				await _entityWriter.WriteEntityAsync(request.Options.FileLocation, request.Entity);

			return Unit.Value;
		}
	}
}
