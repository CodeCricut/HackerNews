using HackerNews.CLI.ApplicationRequests;
using HackerNews.CLI.ApplicationRequests.PostEntityRequests;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class PostArticleHostedService : IHostedService
	{
		private readonly PostArticleOptions _options;
		private readonly IPostEntityRequestHandler<PostArticleModel, GetArticleModel> _requestHandler;

		public PostArticleHostedService(PostArticleOptions options,
			IPostEntityRequestHandler<PostArticleModel, GetArticleModel> postAggreg)
		{
			_options = options;
			_requestHandler = postAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var postArticleRequest = new PostArticleRequest(_options);
			return _requestHandler.HandleAsync(postArticleRequest);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
