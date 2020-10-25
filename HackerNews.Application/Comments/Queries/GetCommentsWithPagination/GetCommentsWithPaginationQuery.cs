using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
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
		public GetCommentsWithPaginationHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<PaginatedList<GetCommentModel>> Handle(GetCommentsWithPaginationQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var comments = await UnitOfWork.Comments.GetEntitiesAsync();
				var paginatedComments = await comments.PaginatedListAsync(request.PagingParams);

				return Mapper.Map<PaginatedList<GetCommentModel>>(paginatedComments);
			}
		}
	}
}
