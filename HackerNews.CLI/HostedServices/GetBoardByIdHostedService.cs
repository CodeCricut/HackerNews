﻿using HackerNews.CLI.ApplicationRequests.GetEntityRequests;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Options;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetBoardByIdHostedService : IHostedService
	{
		private readonly GetBoardByIdOptions _options;
		private readonly IGetEntityRequestHandler<GetBoardModel, BoardInclusionConfiguration> _getRequestAggreg;

		public GetBoardByIdHostedService(
			GetBoardByIdOptions options,
			IGetEntityRequestHandler<GetBoardModel, BoardInclusionConfiguration> getRequestAggreg
			)
		{
			_options = options;
			_getRequestAggreg = getRequestAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new GetBoardRequest(_options);
			return _getRequestAggreg.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
