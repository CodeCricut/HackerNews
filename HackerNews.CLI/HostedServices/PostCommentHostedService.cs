using HackerNews.CLI.ApplicationRequests;
using HackerNews.CLI.ApplicationRequests.PostEntityRequests;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class PostCommentHostedService : IHostedService
	{
		private readonly PostCommentOptions _options;
		private readonly IPostEntityRequestHandler<PostCommentModel, GetCommentModel> _requestHandler;

		public PostCommentHostedService(PostCommentOptions options,
			IPostEntityRequestHandler<PostCommentModel, GetCommentModel> requestHandler)
		{
			_options = options;
			_requestHandler = requestHandler;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var postCommentRequest = new PostCommentRequest(_options);
			return _requestHandler.HandleAsync(postCommentRequest);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
