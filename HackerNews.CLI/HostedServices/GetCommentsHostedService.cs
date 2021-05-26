using HackerNews.CLI.ApplicationRequests.GetEntitiesRequests;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetCommentsHostedService : IHostedService
	{
		private readonly GetCommentsOptions _options;
		private readonly IGetEntitiesRequestHandler<GetCommentModel, CommentInclusionConfiguration> _requestAggreg;

		public GetCommentsHostedService(
			GetCommentsOptions options,
			IGetEntitiesRequestHandler<GetCommentModel, CommentInclusionConfiguration> requestAggreg
			)
		{
			_options = options;
			_requestAggreg = requestAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new GetCommentsRequest(_options);
			return _requestAggreg.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
