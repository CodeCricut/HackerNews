using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetBoards
{
	public class GetBoardsHostedService : IHostedService
	{
		private readonly GetBoardsOptions _options;
		private readonly IGetBoardProcessor _getBoardProcessor;
		private readonly ILogger<GetBoardsHostedService> _logger;

		public GetBoardsHostedService(GetBoardsOptions options,
			IGetBoardProcessor getBoardProcessor,
			ILogger<GetBoardsHostedService> logger)
		{
			_options = options;
			_getBoardProcessor = getBoardProcessor;
			_logger = logger;

			_logger.LogTrace("Created " + this.GetType().Name);
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"Starting {this.GetType().Name} as hosted service.");
			return _getBoardProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"Stopping {this.GetType().Name} as hosted service.");
			return Task.CompletedTask;
		}
	}
}
