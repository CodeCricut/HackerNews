using HackerNews.CLI.ApplicationRequests.GetEntitiesRequests;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetArticlesHostedService : IHostedService
	{
		private readonly GetArticlesOptions _options;
		private readonly IGetEntitiesRequestHandler<GetArticleModel, ArticleInclusionConfiguration> _requestAggreg;

		public GetArticlesHostedService(
			GetArticlesOptions options,
			IGetEntitiesRequestHandler<GetArticleModel, ArticleInclusionConfiguration> requestAggreg
			)
		{
			_options = options;
			_requestAggreg = requestAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new GetArticlesRequest(_options);
			return _requestAggreg.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
