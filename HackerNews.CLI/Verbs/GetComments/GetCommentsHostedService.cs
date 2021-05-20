using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetComments
{
	public class GetCommentsHostedService : IHostedService
	{
		private readonly GetCommentsOptions _options;
		private readonly IGetCommentProcessor _getCommentProcessor;

		public GetCommentsHostedService(GetCommentsOptions options,
			IGetCommentProcessor getCommentProcessor)
		{
			_options = options;
			_getCommentProcessor = getCommentProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _getCommentProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
