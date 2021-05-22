using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Requests.GetArticleById;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetArticleByIdHostedService : IHostedService
	{
		private readonly GetArticleByIdRequest _request;

		public GetArticleByIdHostedService(GetArticleByIdOptions options,
			GetArticleByIdRequestBuilder requestBuilder)
		{
			_request = requestBuilder
				.Configure(options)
				.Build();
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _request.ExecuteAsync();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _request.CancelAsync(cancellationToken);
		}
	}
}
