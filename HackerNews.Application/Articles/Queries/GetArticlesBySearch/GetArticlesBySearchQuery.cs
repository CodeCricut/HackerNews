using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Queries.GetArticlesBySearch
{
	public class GetArticlesBySearchQuery : IRequest<PaginatedList<GetArticleModel>>
	{
		public GetArticlesBySearchQuery(string searchTerm, PagingParams pagingParams)
		{
			SearchTerm = searchTerm;
			PagingParams = pagingParams;
		}

		public string SearchTerm { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetArticlesBySearchHandler : DatabaseRequestHandler<GetArticlesBySearchQuery, PaginatedList<GetArticleModel>>
	{
		public GetArticlesBySearchHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetArticleModel>> Handle(GetArticlesBySearchQuery request, CancellationToken cancellationToken)
		{
			var articles = await UnitOfWork.Articles.GetEntitiesAsync();
			var searchArticles = articles.Where(a =>
				a.Title.Contains(request.SearchTerm) ||
				a.Text.Contains(request.SearchTerm)
			);

			var paginatedSearchedArticles = await PaginatedList<Article>.CreateAsync(searchArticles,
				request.PagingParams.PageNumber, request.PagingParams.PageSize);

			return paginatedSearchedArticles.ToMappedPagedList<Article, GetArticleModel>(Mapper);
		}
	}
}
