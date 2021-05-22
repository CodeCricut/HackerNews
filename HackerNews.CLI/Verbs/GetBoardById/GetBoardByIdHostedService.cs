using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetBoardById
{
	public class GetBoardByIdHostedService : IHostedService
	{
		private readonly GetBoardByIdOptions _options;
		private GetBoardByIdRequest _request;

		public GetBoardByIdHostedService(
			GetBoardByIdOptions options,
			GetBoardByIdRequestBuilder getBoardByIdRequestBuilder
			)
		{
			_options = options;

			_request = getBoardByIdRequestBuilder
				.Configure(_options)
				.ConfigureVerbosity(() => true) // overrides previous configuration
				.Build();
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _request.ExecuteAsync();
		}
		
		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _request.CancelAsync(cancellationToken);
		}
	}
}
