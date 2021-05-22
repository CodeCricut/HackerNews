using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.PostBoard;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class PostBoardHostedService : IHostedService
	{
		private readonly PostBoardRequest _request;

		public PostBoardHostedService(PostBoardOptions options,
			PostBoardRequestBuilder requestBuilder)
		{
			_request = requestBuilder
				.Configure(options)
				.Build();
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _request.ExecuteAsync();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _request.CancelAsync(cancellationToken);
		}
	}
}
