using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetPublicUsers
{
	public class GetPublicUsersVerb : IHostedService
	{
		private readonly GetPublicUsersVerbOptions _options;
		private readonly IGetPublicUserProcessor _getPublicUserProcessor;

		public GetPublicUsersVerb(GetPublicUsersVerbOptions options,
			IGetPublicUserProcessor getPublicUserProcessor)
		{
			_options = options;
			_getPublicUserProcessor = getPublicUserProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _getPublicUserProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
