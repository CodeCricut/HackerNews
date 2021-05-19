using CommandLine;
using HackerNews.CLI.Verbs.Post;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.PostBoard
{
	[Verb("post-b", HelpText = "Post a board.")]
	public class PostBoardVerbOptions : PostEntityVerbOptions, IPostEntityVerbOptions
	{
		[Option("Title", SetName = "boards", HelpText = "The title to assign to the board being posted")]
		public string Title { get; set; }
		[Option("Description", SetName = "boards", HelpText = "The description to assign to the board being posted")]
		public string Description { get; set; }
	}

	public class PostBoardVerb : IHostedService
	{
		private readonly PostBoardVerbOptions _options;
		private readonly IPostBoardProcessor _postBoardProcessor;

		public PostBoardVerb(PostBoardVerbOptions options,
			IPostBoardProcessor postBoardProcessor)
		{
			_options = options;
			_postBoardProcessor = postBoardProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _postBoardProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
