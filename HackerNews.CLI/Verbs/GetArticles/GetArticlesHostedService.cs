using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetArticles
{
	public class GetArticlesHostedService : IHostedService
	{
		private readonly GetArticleOptions _options;
		private readonly IGetArticleProcessor _getArticleProcessor;
		private readonly ILogger<GetArticlesHostedService> _logger;

		public GetArticlesHostedService(GetArticleOptions options,
			IGetArticleProcessor getArticleProcessor,
			ILogger<GetArticlesHostedService> logger)
		{
			_options = options;
			_getArticleProcessor = getArticleProcessor;
			_logger = logger;

			logger.LogTrace("Created " + this.GetType().Name);
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Starting " + this.GetType().Name + " as hosted service.");
			return _getArticleProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Stopping " + this.GetType().Name + " as hosted service.");
			return Task.CompletedTask;
		}
	}
}
