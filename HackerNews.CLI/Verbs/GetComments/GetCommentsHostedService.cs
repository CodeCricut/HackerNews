using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetComments
{
	public class GetCommentsHostedService : IHostedService
	{
		private readonly GetCommentsOptions _options;
		private readonly IGetCommentProcessor _getCommentProcessor;
		private readonly ILogger<GetCommentsHostedService> _logger;

		public GetCommentsHostedService(GetCommentsOptions options,
			IGetCommentProcessor getCommentProcessor,
			ILogger<GetCommentsHostedService> logger)
		{
			_options = options;
			_getCommentProcessor = getCommentProcessor;
			_logger = logger;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Starting " + this.GetType().Name + " as hosted service.");

			return _getCommentProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Stopping " + this.GetType().Name + " as hosted service.");

			return Task.CompletedTask;
		}
	}
}
