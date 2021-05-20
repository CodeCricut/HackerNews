using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetBoards
{
	public class GetBoardsVerb : IHostedService
	{
		private readonly GetBoardsVerbOptions _options;
		private readonly IGetBoardProcessor _getBoardProcessor;

		public GetBoardsVerb(GetBoardsVerbOptions options,
			IGetBoardProcessor getBoardProcessor)
		{
			_options = options;
			_getBoardProcessor = getBoardProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _getBoardProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
