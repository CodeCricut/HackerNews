using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.MediatR;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Queries.GetEntityById
{
	public class GetArticleByIdQuery : GetEntityByIdQuery<GetArticleModel>
	{
		public GetArticleByIdQuery(IIdOptions options) : base(options)
		{
		}
	}

	public class GetArticleByIdQueryHandler : GetEntityByIdQueryHandler<GetArticleByIdQuery, GetArticleModel>
	{
		public GetArticleByIdQueryHandler(ILogger<GetEntityByIdQueryHandler<GetArticleByIdQuery, GetArticleModel>> logger, IEntityFinder<GetArticleModel> entityFinder) : base(logger, entityFinder)
		{
		}
	}
}
