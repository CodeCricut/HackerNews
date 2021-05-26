using HackerNews.CLI.ApplicationRequests.GetEntitiesRequests;
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
	public class GetPublicUsersHostedService : IHostedService
	{
		private readonly GetPublicUsersOptions _options;
		private readonly IGetEntitiesRequestHandler<GetPublicUserModel, PublicUserInclusionConfiguration> _requestAggreg;

		public GetPublicUsersHostedService(
			GetPublicUsersOptions options,
			IGetEntitiesRequestHandler<GetPublicUserModel, PublicUserInclusionConfiguration> requestAggreg
			)
		{
			_options = options;
			_requestAggreg = requestAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new GetPublicUsersRequest(_options);
			return _requestAggreg.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
