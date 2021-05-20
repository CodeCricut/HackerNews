using CommandLine;
using HackerNews.CLI.Verbs.Post;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.PostComment
{
	public class PostCommentHostedService : IHostedService
	{
		private readonly PostCommentOptions _options;
		private readonly IPostCommentProcessor _postCommentProcessor;

		public PostCommentHostedService(PostCommentOptions options,
			IPostCommentProcessor postCommentProcessor)
		{
			_options = options;
			_postCommentProcessor = postCommentProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _postCommentProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
