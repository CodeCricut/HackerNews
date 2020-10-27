﻿using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public GetBoardsBySearchHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetBoardModel>> Handle(GetBoardsBySearchQuery request, CancellationToken cancellationToken)
		{
			var boards = await UnitOfWork.Boards.GetEntitiesAsync();
			var searchedBoards = boards.Where(b =>
				b.Title.Contains(request.SearchTerm) ||
				b.Description.Contains(request.SearchTerm)
			);

			var paginatedSearchedBoards = await PaginatedList<Board>.CreateAsync(searchedBoards, 
				request.PagingParams.PageNumber, request.PagingParams.PageSize);

			return paginatedSearchedBoards.ToMappedPagedList<Board, GetBoardModel>(Mapper);
		}
	}
}