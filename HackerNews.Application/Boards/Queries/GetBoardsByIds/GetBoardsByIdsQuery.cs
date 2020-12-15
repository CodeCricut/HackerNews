using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly IDeletedEntityPolicyValidator<Board> _deletedBoardValidator;

		public GetBoardsByIdsHandler(IDeletedEntityPolicyValidator<Board> deletedBoardValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedBoardValidator = deletedBoardValidator;
		}

		public override async Task<PaginatedList<GetBoardModel>> Handle(GetBoardsByIdsQuery request, CancellationToken cancellationToken)
		{
			var boards = await UnitOfWork.Boards.GetEntitiesAsync();
			var boardsByIds = boards.Where(b => request.Ids.Contains(b.Id));

			boardsByIds = _deletedBoardValidator.ValidateEntityQuerable(boardsByIds, Domain.Common.DeletedEntityPolicy.OWNER);

			var paginatedBoards = await boardsByIds.PaginatedListAsync(request.PagingParams);

			return paginatedBoards.ToMappedPagedList<Board, GetBoardModel>(Mapper);
		}
	}
}
