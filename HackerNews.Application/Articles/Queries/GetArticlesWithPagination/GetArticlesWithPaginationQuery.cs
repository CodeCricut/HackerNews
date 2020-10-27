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
		public GetArticlesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetArticleModel>> Handle(GetArticlesWithPaginationQuery request, CancellationToken cancellationToken)
		{
			var articles = await UnitOfWork.Articles.GetEntitiesAsync();
			var paginatedArticles = await articles.PaginatedListAsync(request.PagingParams);

			return paginatedArticles.ToMappedPagedList<Article, GetArticleModel>(Mapper);
		}
	}
}
