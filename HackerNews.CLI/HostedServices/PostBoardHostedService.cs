﻿using HackerNews.CLI.ApplicationRequests;
using HackerNews.CLI.Options;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class PostBoardHostedService : IHostedService
	{
		private readonly PostBoardOptions _options;
		private readonly IPostEntityRequestHandler<PostBoardModel, GetBoardModel> _requestHandler;

		public PostBoardHostedService(PostBoardOptions options,
			IPostEntityRequestHandler<PostBoardModel, GetBoardModel> postAggreg)
		{
			_options = options;
			_requestHandler = postAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var postBoardRequest = new PostBoardRequest(_options);
			return _requestHandler.HandleAsync(postBoardRequest);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
