using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Queries.GetEntitiesByIds
{
	public class GetArticlesByIdsQuery : GetEntitiesByIdsQuery<GetArticleModel>
	{
		public GetArticlesByIdsQuery(IIdListOptions idsOptions, IPageOptions pageOptions) : base(idsOptions, pageOptions)
		{
		}
	}

	public class GetArticlesByIdsQueryHandler : GetEntitiesByIdsQueryHandler<GetArticlesByIdsQuery, GetArticleModel>
	{
		public GetArticlesByIdsQueryHandler(IEntityFinder<GetArticleModel> entityFinder) : base(entityFinder)
		{
		}
	}
}
