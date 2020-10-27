using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Comments;
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

namespace HackerNews.Application.Comments.Queries.GetCommentsBySearch
{
	public class GetCommentsBySearchQuery : IRequest<PaginatedList<GetCommentModel>>
	{
		public GetCommentsBySearchQuery(string searchTerm, PagingParams pagingParams)
		{
			SearchTerm = searchTerm;
			PagingParams = pagingParams;
		}

		public string SearchTerm { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetCommentsBySearchHandler : DatabaseRequestHandler<GetCommentsBySearchQuery, PaginatedList<GetCommentModel>>
	{
		public GetCommentsBySearchHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetCommentModel>> Handle(GetCommentsBySearchQuery request, CancellationToken cancellationToken)
		{
			var comments = await UnitOfWork.Comments.GetEntitiesAsync();
			var searchedComments = comments.Where(c =>
				c.Text.Contains(request.SearchTerm)
			);

			var paginatedSearchedComments = await PaginatedList<Comment>.CreateAsync(searchedComments,
				request.PagingParams.PageNumber, request.PagingParams.PageSize);

			return paginatedSearchedComments.ToMappedPagedList<Comment, GetCommentModel>(Mapper);
		}
	}
}
