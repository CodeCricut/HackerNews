using HackerNews.CLI.Options;
using HackerNews.CLI.Verbs.GetBoardById;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetBoardByIdHostedService : IHostedService
	{
		private GetBoardByIdRequest _request;

		public GetBoardByIdHostedService(
			GetBoardByIdOptions options,
			GetBoardByIdRequestBuilder getBoardByIdRequestBuilder
			)
		{
			_request = getBoardByIdRequestBuilder
				.Configure(options)
				.OverrideVerbosity.SetWhenBuilt(() => true)
				.Build();
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _request.ExecuteAsync();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _request.CancelAsync(cancellationToken);
		}
	}
}
