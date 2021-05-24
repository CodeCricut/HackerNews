using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Queries.GetEntitiesByIds
{
	public class GetBoardsByIdsQuery : GetEntitiesByIdsQuery<GetBoardModel>, IRequest<PaginatedList<GetBoardModel>>
	{
		public GetBoardsByIdsQuery(IIdListOptions idsOptions, IPageOptions pageOptions) : base(idsOptions, pageOptions)
		{
		}
	}

	public class GetBoardsByIdsQueryHandler : GetEntitiesByIdsQueryHandler<GetBoardModel>
	{
		public GetBoardsByIdsQueryHandler(IEntityFinder<GetBoardModel> entityFinder) : base(entityFinder)
		{
		}

		public Task<PaginatedList<GetBoardModel>> Handle(GetBoardsByIdsQuery request, CancellationToken cancellationToken)
		{
			return base.GetEntitiesByIdsAsync(request);
		}
	}
}
