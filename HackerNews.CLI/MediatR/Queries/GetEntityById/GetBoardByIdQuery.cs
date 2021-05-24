using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Queries.GetEntityById
{
	public class GetBoardByIdQuery : GetEntityByIdQuery<GetBoardModel>, IRequest<GetBoardModel>
	{
		public GetBoardByIdQuery(IIdOptions options) : base(options)
		{
		}
	}

	public class GetBoardByIdQueryHandler : GetEntityByIdQueryHandler<GetBoardModel>,
		IRequestHandler<GetBoardByIdQuery, GetBoardModel>
	{
		public GetBoardByIdQueryHandler(ILogger<GetEntityByIdQueryHandler<GetBoardModel>> logger, IEntityFinder<GetBoardModel> entityFinder) : base(logger, entityFinder)
		{
		}

		public Task<GetBoardModel> Handle(GetBoardByIdQuery request, CancellationToken cancellationToken)
		{
			return base.GetByIdAsync(request);
		}
	}
}
