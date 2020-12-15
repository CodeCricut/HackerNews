using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
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
		private readonly IDeletedEntityPolicyValidator<Article> _deletedEntityValidator;

		public GetArticlesByIdsQueryHandler(IDeletedEntityPolicyValidator<Article> deletedEntityValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedEntityValidator = deletedEntityValidator;
		}

		public override async Task<PaginatedList<GetArticleModel>> Handle(GetArticlesByIdsQuery request, CancellationToken cancellationToken)
		{
			var articles = await UnitOfWork.Articles.GetEntitiesAsync();
			var articlesByIds = articles.Where(article => request.Ids.Contains(article.Id));

			articlesByIds = _deletedEntityValidator.ValidateEntityQuerable(articlesByIds, Domain.Common.DeletedEntityPolicy.OWNER);

			var paginatedArticles = await articlesByIds.PaginatedListAsync(request.PagingParams);

			return paginatedArticles.ToMappedPagedList<Article, GetArticleModel>(Mapper);
		}
	}
}
