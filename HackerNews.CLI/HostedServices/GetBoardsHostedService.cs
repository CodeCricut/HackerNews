using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.GetBoards;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetBoardsHostedService : IHostedService
	{
		private GetBoardsRequest _request;

		public GetBoardsHostedService(
			GetBoardsOptions options,
			GetBoardsRequestBuilder requestBuilder
			)
		{
			_request = requestBuilder
				.Configure(options)
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