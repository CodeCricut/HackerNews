using HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration;
using HackerNews.CLI.Application.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public class GetArticlesRequest : IGetEntitiesRequest<GetArticleModel, ArticleInclusionConfiguration>
	{
		public CreateVerbosityCommand CreateVerbosityCommand {get;}

		public CreateGetEntitiesByIdsQuery<GetArticleModel> CreateGetEntitiesByIdsQuery {get;}

		public CreateLogEntityPageCommand<GetArticleModel, ArticleInclusionConfiguration> CreateLogEntityPageCommand {get;}

		public CreateWriteEntityPageCommand<GetArticleModel, ArticleInclusionConfiguration> CreateWriteEntityPageCommand {get;}

		public GetArticlesRequest(GetArticlesOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntitiesByIdsQuery = () => new GetArticlesByIdsQuery(opts, opts);
			CreateLogEntityPageCommand = articlePage => new LogArticlePageCommand(articlePage, opts, opts.ToInclusionConfiguration());
			CreateWriteEntityPageCommand = articlePage => new WriteArticlePageCommand(articlePage, opts, opts.ToInclusionConfiguration());
		}		
	}
}
