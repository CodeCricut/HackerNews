using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.MediatR.Queries.GetEntityById
{
	public class GetBoardByIdQuery : GetEntityByIdQuery<GetBoardModel>
	{
		public GetBoardByIdQuery(IIdOptions options) : base(options)
		{
		}
	}

	public class GetBoardByIdQueryHandler :
		GetEntityByIdQueryHandler<GetBoardByIdQuery, GetBoardModel>
	{
		public GetBoardByIdQueryHandler(ILogger<GetBoardByIdQueryHandler> logger, IEntityFinder<GetBoardModel> entityFinder) : base(logger, entityFinder)
		{
		}
	}
}
