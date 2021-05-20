using CommandLine;
using HackerNews.CLI.Verbs.Post;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.PostBoard
{
	public class PostBoardHostedService : IHostedService
	{
		private readonly PostBoardOptions _options;
		private readonly IPostBoardProcessor _postBoardProcessor;

		public PostBoardHostedService(PostBoardOptions options,
			IPostBoardProcessor postBoardProcessor)
		{
			_options = options;
			_postBoardProcessor = postBoardProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _postBoardProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
