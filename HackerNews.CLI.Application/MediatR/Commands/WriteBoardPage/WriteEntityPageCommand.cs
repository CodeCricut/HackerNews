using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.WriteBoardPage
{
	public class WriteEntityPageCommand<TGetModel> : IRequest
		where TGetModel : GetModelDto
	{
		public WriteEntityPageCommand(PaginatedList<TGetModel> entityPage, IFileOptions fileOptions)
		{
			EntityPage = entityPage;
			FileOptions = fileOptions;
		}

		public PaginatedList<TGetModel> EntityPage { get; }
		public IFileOptions FileOptions { get; }
	}

	public class WriteEntityPageCommandHandler<TRequest, TGetModel> :
		IRequestHandler<TRequest>
		where TRequest : WriteEntityPageCommand<TGetModel>
		where TGetModel : GetModelDto
	{
		private readonly IEntityWriter<TGetModel> _entityWriter;

		public WriteEntityPageCommandHandler(IEntityWriter<TGetModel> entityWriter)
		{
			_entityWriter = entityWriter;
		}

		public virtual async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
		{

			if (!string.IsNullOrEmpty(request.FileOptions.FileLocation))
				await _entityWriter.WriteEntityPageAsync(request.FileOptions.FileLocation, request.EntityPage);

			return Unit.Value;
		}
	}
}
