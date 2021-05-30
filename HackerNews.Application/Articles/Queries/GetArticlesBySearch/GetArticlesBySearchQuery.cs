using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
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
		private readonly IDeletedEntityPolicyValidator<Article> _deletedEntityValidator;

		public GetArticlesBySearchHandler(IDeletedEntityPolicyValidator<Article> deletedEntityValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedEntityValidator = deletedEntityValidator;
		}

		public override async Task<PaginatedList<GetArticleModel>> Handle(GetArticlesBySearchQuery request, CancellationToken cancellationToken)
		{
			IQueryable<Article> searchArticles = await GetArticlesBySearch(request.SearchTerm);

			searchArticles = _deletedEntityValidator.ValidateEntityQuerable(searchArticles, Domain.Common.DeletedEntityPolicy.OWNER);

			var paginatedSearchedArticles = await searchArticles.PaginatedListAsync(request.PagingParams);

			return paginatedSearchedArticles.ToMappedPagedList<Article, GetArticleModel>(Mapper);
		}

		private async Task<IQueryable<Article>> GetArticlesBySearch(string searchTerm)
		{
			var articles = await UnitOfWork.Articles.GetEntitiesAsync();
			var searchArticles = articles.Where(a =>
				a.Title.Contains(searchTerm) ||
				a.Text.Contains(searchTerm)
			);
			return searchArticles;
		}
	}
}
