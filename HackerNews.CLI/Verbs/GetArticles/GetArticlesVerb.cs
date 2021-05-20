using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetArticles
{
	public class GetArticlesVerb : IHostedService
	{
		private readonly GetArticlesVerbOptions _options;
		private readonly IGetArticleProcessor _getArticleProcessor;

		public GetArticlesVerb(GetArticlesVerbOptions options,
			IGetArticleProcessor getArticleProcessor)
		{
			_options = options;
			_getArticleProcessor = getArticleProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _getArticleProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
