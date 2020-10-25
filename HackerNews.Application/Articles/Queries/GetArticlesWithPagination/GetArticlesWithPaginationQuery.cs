using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Queries.GetArticlesWithPagination
{
	public class GetArticlesWithPaginationQuery : IRequest<PaginatedList<GetArticleModel>>
	{
		public GetArticlesWithPaginationQuery(PagingParams pagingParams)
		{
			PagingParams = pagingParams;
		}

		public PagingParams PagingParams { get; }
	}

	public class GetArticlesWithPaginationQueryHandler : DatabaseRequestHandler<GetArticlesWithPaginationQuery, PaginatedList<GetArticleModel>>
	{
		public GetArticlesWithPaginationQueryHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<PaginatedList<GetArticleModel>> Handle(GetArticlesWithPaginationQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var articles = await UnitOfWork.Articles.GetEntitiesAsync();
				var paginatedArticles = await articles.PaginatedListAsync(request.PagingParams);

				return Mapper.Map<PaginatedList<GetArticleModel>>(paginatedArticles);
			}
		}
	}
}
