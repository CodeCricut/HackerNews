using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Queries.GetBoardsWithPagination
{
	public class GetBoardsWithPaginationQuery : IRequest<PaginatedList<GetBoardModel>>
	{
		public GetBoardsWithPaginationQuery(PagingParams pagingParams)
		{
			PagingParams = pagingParams;
		}

		public PagingParams PagingParams { get; }
	}

	public class GetBoardsWithPaginationHandler : DatabaseRequestHandler<GetBoardsWithPaginationQuery, PaginatedList<GetBoardModel>>
	{
		public GetBoardsWithPaginationHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<PaginatedList<GetBoardModel>> Handle(GetBoardsWithPaginationQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var boards = await UnitOfWork.Boards.GetEntitiesAsync();
				var paginatedBoards = await boards.PaginatedListAsync(request.PagingParams);

				return paginatedBoards.ToMappedPagedList<Board, GetBoardModel>(Mapper);
			}
		}
	}
}
