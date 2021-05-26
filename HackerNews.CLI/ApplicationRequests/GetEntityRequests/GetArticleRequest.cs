using HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration;
using HackerNews.CLI.Application.MediatR.Queries.GetEntityById;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public class GetArticleRequest : IGetEntityRequest<GetArticleModel, ArticleInclusionConfiguration>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntityQuery<GetArticleModel> CreateGetEntityQuery { get; }

		public CreateLogEntityCommand<GetArticleModel, ArticleInclusionConfiguration> CreateLogEntityCommand { get; }

		public CreateWriteEntityCommand<GetArticleModel, ArticleInclusionConfiguration> CreateWriteEntityCommand { get; }

		public GetArticleRequest(GetArticleByIdOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntityQuery = () => new GetArticleByIdQuery(opts);
			CreateLogEntityCommand = article => new LogArticleCommand(article, opts, opts.ToInclusionConfiguration());
			CreateWriteEntityCommand = article => new WriteArticleCommand(article, opts, opts.ToInclusionConfiguration());
		}
	}
}
