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
		private readonly IDeletedEntityPolicyValidator<Article> _deletedEntityValidator;

		public GetArticlesWithPaginationQueryHandler(IDeletedEntityPolicyValidator<Article> deletedEntityValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedEntityValidator = deletedEntityValidator;
		}

		public override async Task<PaginatedList<GetArticleModel>> Handle(GetArticlesWithPaginationQuery request, CancellationToken cancellationToken)
		{
			var articles = await UnitOfWork.Articles.GetEntitiesAsync();

			articles = _deletedEntityValidator.ValidateEntityQuerable(articles, Domain.Common.DeletedEntityPolicy.OWNER);
			
			var paginatedArticles = await articles.PaginatedListAsync(request.PagingParams);

			return paginatedArticles.ToMappedPagedList<Article, GetArticleModel>(Mapper);
		}
	}
}
