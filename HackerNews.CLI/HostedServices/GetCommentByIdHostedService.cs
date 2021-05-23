using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Requests.GetCommentById;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetCommentByIdHostedService : IHostedService
	{
		private readonly GetCommentByIdRequest _request;

		public GetCommentByIdHostedService(
			GetCommentByIdOptions options,
			GetCommentByIdRequestBuilder requestBuilder)
		{
			_request = requestBuilder
				.Configure(options)
				.OverrideOptions
					.SetId(1)
					.Se
				.IdOptions.SetValue(1)
				.

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
