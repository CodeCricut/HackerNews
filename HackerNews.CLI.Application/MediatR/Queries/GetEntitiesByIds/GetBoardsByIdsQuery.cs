using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.MediatR.Queries.GetEntitiesByIds
{
	public class GetBoardsByIdsQuery : GetEntitiesByIdsQuery<GetBoardModel>
	{
		public GetBoardsByIdsQuery(IIdListOptions idsOptions, IPageOptions pageOptions) : base(idsOptions, pageOptions)
		{
		}
	}

	public class GetBoardsByIdsQueryHandler : GetEntitiesByIdsQueryHandler<GetBoardsByIdsQuery, GetBoardModel>
	{
		public GetBoardsByIdsQueryHandler(IEntityFinder<GetBoardModel> entityFinder) : base(entityFinder)
		{
		}
	}
}
