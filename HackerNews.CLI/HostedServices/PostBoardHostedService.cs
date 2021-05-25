using HackerNews.CLI.ApplicationRequests;
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
		private readonly IPostEntityRequestAggregator<PostBoardModel, GetBoardModel> _postAggreg;

		public PostBoardHostedService(PostBoardOptions options,
			IPostEntityRequestAggregator<PostBoardModel, GetBoardModel> postAggreg)
		{
			_options = options;
			_postAggreg = postAggreg;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var postBoardRequest = new PostBoardRequest(_options);
			return _postAggreg.HandleAsync(postBoardRequest);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
