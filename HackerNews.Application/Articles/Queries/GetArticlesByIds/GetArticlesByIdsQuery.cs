using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Queries.GetArticlesByIds
{
	public class GetArticlesByIdsQuery : IRequest<PaginatedList<GetArticleModel>>
	{
		public GetArticlesByIdsQuery(IEnumerable<int> ids, PagingParams pagingParams)
		{
			Ids = ids;
			PagingParams = pagingParams;
		}

		public IEnumerable<int> Ids { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetArticlesByIdsQueryHandler : DatabaseRequestHandler<GetArticlesByIdsQuery, PaginatedList<GetArticleModel>>
	{
		public GetArticlesByIdsQueryHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<PaginatedList<GetArticleModel>> Handle(GetArticlesByIdsQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var articles = await UnitOfWork.Articles.GetEntitiesAsync();
				var articlesByIds = articles.Where(article => request.Ids.Contains(article.Id));
				var paginatedArticles = await articlesByIds.PaginatedListAsync(request.PagingParams);

				return Mapper.Map<PaginatedList<GetArticleModel>>(paginatedArticles);
			}
		}
	}
}
