using CommandLine;
using HackerNews.CLI.Verbs.Post;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.PostArticle
{
	public class PostArticleHostedService : IHostedService
	{
		private readonly PostArticleOptions _options;
		private readonly IPostArticleProcessor _postArticleProcessor;

		public PostArticleHostedService(PostArticleOptions options,
			IPostArticleProcessor postArticleProcessor)
		{
			_options = options;
			_postArticleProcessor = postArticleProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _postArticleProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
