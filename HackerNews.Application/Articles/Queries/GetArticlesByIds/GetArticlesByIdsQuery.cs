using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
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
		public GetArticlesByIdsQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetArticleModel>> Handle(GetArticlesByIdsQuery request, CancellationToken cancellationToken)
		{
			var articles = await UnitOfWork.Articles.GetEntitiesAsync();
			var articlesByIds = articles.Where(article => request.Ids.Contains(article.Id));
			var paginatedArticles = await articlesByIds.PaginatedListAsync(request.PagingParams);

			// Second bug caught by tests!
			return paginatedArticles.ToMappedPagedList<Article, GetArticleModel>(Mapper);
		}
	}
}
