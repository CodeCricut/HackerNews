using HackerNews.CLI.ApplicationRequests.GetEntityRequests;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Options.Verbs;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetArticleByIdHostedService : IHostedService
	{
		private readonly GetArticleByIdOptions _options;
		private readonly IGetEntityRequestHandler<GetArticleModel, ArticleInclusionConfiguration> _getRequestAggreg;

		public GetArticleByIdHostedService(
			GetArticleByIdOptions options,
			IGetEntityRequestHandler<GetArticleModel, ArticleInclusionConfiguration> getRequestAggreg
			)
		{
			_options = options;
			_getRequestAggreg = getRequestAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new GetArticleRequest(_options);
			return _getRequestAggreg.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
