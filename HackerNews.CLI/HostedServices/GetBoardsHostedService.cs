using HackerNews.CLI.ApplicationRequests.GetEntitiesRequests;
using HackerNews.CLI.Options;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetBoardsHostedService : IHostedService
	{
		private readonly GetBoardsOptions _options;
		private readonly IGetEntitiesRequestHandler<GetBoardModel> _requestAggreg;

		public GetBoardsHostedService(
			GetBoardsOptions options,
			IGetEntitiesRequestHandler<GetBoardModel> requestAggreg
			)
		{
			_options = options;
			_requestAggreg = requestAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new GetBoardsRequest(_options);
			return _requestAggreg.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}