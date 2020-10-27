using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
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
		public GetBoardsByIdsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetBoardModel>> Handle(GetBoardsByIdsQuery request, CancellationToken cancellationToken)
		{
				var boards = await UnitOfWork.Boards.GetEntitiesAsync();
				var boardsByIds = boards.Where(b => request.Ids.Contains(b.Id));

				var paginatedBoards = await boardsByIds.PaginatedListAsync(request.PagingParams);

			return paginatedBoards.ToMappedPagedList<Board, GetBoardModel>(Mapper);
		}
	}
}
