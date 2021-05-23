using HackerNews.CLI.Requests.EntityRequest;
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
			GetBoardByIdRequestBuilder builder = getBoardByIdRequestBuilder
				.Configure(options)
				.Options
					.SetIncludeAll(true)
					.SetFileLocation("....")
					.SetId(1)
					.SetVerbosity(false);

			builder.BuildActions.Add(() => { });

			_request = builder.Build();
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
