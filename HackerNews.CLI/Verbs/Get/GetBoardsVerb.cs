using CommandLine;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	[Verb("get", HelpText = "Get boards from the database.")]
	public class GetBoardsVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		// Distinguishes this from other get verbs
		[Option('b', "boards", Required = true, HelpText = "Get boards.")]
		public bool GetBoards { get; set; }

		[Option("includeBoardId", SetName = "boards")]
		public bool IncludeBoardId { get; set; }
		[Option("includeBoardTitle", SetName = "boards")]
		public bool IncludeBoardTitle { get; set; }
		[Option("includeBoardDescription", SetName = "boards")]
		public bool IncludeBoardDescription { get; set; }
		[Option("includeBoardCreateDate", SetName = "boards")]
		public bool IncludeBoardCreateDate { get; set; }
		[Option("includeBoardCreatorId", SetName = "boards")]
		public bool IncludeBoardCreatorId { get; set; }
		[Option("includeBoardModeratorIds", SetName = "boards")]
		public bool IncludeBoardModeratorIds { get; set; }
		[Option("includeBoardSubscriberIds", SetName = "boards")]
		public bool IncludeBoardSubscriberIds { get; set; }
		[Option("includeBoardArticleIds", SetName = "boards")]
		public bool IncludeBoardArticleIds { get; set; }
		[Option("includeBoardDeleted", SetName = "boards")]
		public bool IncludeBoardDeleted { get; set; }
		[Option("includeBoardImageId", SetName = "boards")]
		public bool IncludeBoardImageId { get; set; }
	}

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
