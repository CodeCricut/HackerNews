using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Queries.GetBoardsByIds
{
	public class GetBoardsByIdsQuery : IRequest<PaginatedList<GetBoardModel>>
	{
		public GetBoardsByIdsQuery(IEnumerable<int> ids, PagingParams pagingParams)
		{
			Ids = ids;
			PagingParams = pagingParams;
		}

		public IEnumerable<int> Ids { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetBoardsByIdsHandler : DatabaseRequestHandler<GetBoardsByIdsQuery, PaginatedList<GetBoardModel>>
	{
		public GetBoardsByIdsHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<PaginatedList<GetBoardModel>> Handle(GetBoardsByIdsQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var boards = await UnitOfWork.Boards.GetEntitiesAsync();
				var boardsByIds = boards.Where(b => request.Ids.Contains(b.Id));

				var paginatedBoards = boardsByIds.PaginatedListAsync(request.PagingParams);

				return Mapper.Map<PaginatedList<GetBoardModel>>(paginatedBoards);
			}
		}
	}
}
