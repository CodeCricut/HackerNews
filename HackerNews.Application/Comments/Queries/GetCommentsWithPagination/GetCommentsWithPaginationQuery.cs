using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Comments.Queries.GetCommentsWithPagination
{
	public class GetCommentsWithPaginationQuery : IRequest<PaginatedList<GetCommentModel>>
	{
		public GetCommentsWithPaginationQuery(PagingParams pagingParams)
		{
			PagingParams = pagingParams;
		}

		public PagingParams PagingParams { get; }
	}

	public class GetCommentsWithPaginationHandler : DatabaseRequestHandler<GetCommentsWithPaginationQuery, PaginatedList<GetCommentModel>>
	{
		public GetCommentsWithPaginationHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetCommentModel>> Handle(GetCommentsWithPaginationQuery request, CancellationToken cancellationToken)
		{
				var comments = await UnitOfWork.Comments.GetEntitiesAsync();
				var paginatedComments = await comments.PaginatedListAsync(request.PagingParams);

				return paginatedComments.ToMappedPagedList<Comment, GetCommentModel>(Mapper);
		}
	}
}
