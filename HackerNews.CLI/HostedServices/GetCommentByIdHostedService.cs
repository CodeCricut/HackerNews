using HackerNews.CLI.ApplicationRequests.GetEntityRequests;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Options.Verbs;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetCommentByIdHostedService : IHostedService
	{
		private readonly GetCommentByIdOptions _options;
		private readonly IGetEntityRequestHandler<GetCommentModel, CommentInclusionConfiguration> _requestHandler;

		public GetCommentByIdHostedService(
			GetCommentByIdOptions options,
			IGetEntityRequestHandler<GetCommentModel, CommentInclusionConfiguration> getRequestAggreg
			)
		{
			_options = options;
			_requestHandler = getRequestAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new GetCommentRequest(_options);
			return _requestHandler.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
