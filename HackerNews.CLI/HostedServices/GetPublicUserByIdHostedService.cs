using HackerNews.CLI.ApplicationRequests.GetEntityRequests;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetPublicUserByIdHostedService : IHostedService
	{
		private readonly GetPublicUserByIdOptions _options;
		private readonly IGetEntityRequestHandler<GetPublicUserModel, PublicUserInclusionConfiguration> _requestHandler;

		public GetPublicUserByIdHostedService(
			GetPublicUserByIdOptions options,
			IGetEntityRequestHandler<GetPublicUserModel, PublicUserInclusionConfiguration> requestHandler
			)
		{
			_options = options;
			_requestHandler = requestHandler;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new GetPublicUserRequest(_options);
			return _requestHandler.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
