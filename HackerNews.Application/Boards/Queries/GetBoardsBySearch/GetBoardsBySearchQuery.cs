using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Queries.GetBoardsBySearch
{
	public class GetBoardsBySearchQuery : IRequest<PaginatedList<GetBoardModel>>
	{
		public GetBoardsBySearchQuery(string searchTerm, PagingParams pagingParams)
		{
			SearchTerm = searchTerm;
			PagingParams = pagingParams;
		}

		public string SearchTerm { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetBoardsBySearchHandler : DatabaseRequestHandler<GetBoardsBySearchQuery, PaginatedList<GetBoardModel>>
	{
		private readonly IDeletedEntityPolicyValidator<Board> _deletedBoardValidator;

		public GetBoardsBySearchHandler(IDeletedEntityPolicyValidator<Board> deletedBoardValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedBoardValidator = deletedBoardValidator;
		}

		public override async Task<PaginatedList<GetBoardModel>> Handle(GetBoardsBySearchQuery request, CancellationToken cancellationToken)
		{
			var boards = await UnitOfWork.Boards.GetEntitiesAsync();

			var searchedBoards = boards.Where(b =>
				b.Title.Contains(request.SearchTerm) ||
				b.Title.Contains(request.SearchTerm) ||
				b.Description.Contains(request.SearchTerm)
			);

			searchedBoards = _deletedBoardValidator.ValidateEntityQuerable(searchedBoards, Domain.Common.DeletedEntityPolicy.OWNER);

			var paginatedSearchedBoards = await PaginatedList<Board>.CreateAsync(searchedBoards,
				request.PagingParams.PageNumber, request.PagingParams.PageSize);

			return paginatedSearchedBoards.ToMappedPagedList<Board, GetBoardModel>(Mapper);
		}
	}
}
