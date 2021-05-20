using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetPublicUsers
{
	public class GetPublicUsersHostedService : IHostedService
	{
		private readonly GetPublicUsersOptions _options;
		private readonly IGetPublicUserProcessor _getPublicUserProcessor;
		private readonly ILogger<GetPublicUsersHostedService> _logger;

		public GetPublicUsersHostedService(GetPublicUsersOptions options,
			IGetPublicUserProcessor getPublicUserProcessor,
			ILogger<GetPublicUsersHostedService> logger)
		{
			_options = options;
			_getPublicUserProcessor = getPublicUserProcessor;
			_logger = logger;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Starting " + this.GetType().Name + " as hosted service.");

			return _getPublicUserProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Stopping " + this.GetType().Name + " as hosted service.");

			return Task.CompletedTask;
		}
	}
}
