using CommandLine;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	[Verb("get-b", HelpText = "Get boards from the database.")]
	public class GetBoardsVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		//// Distinguishes this from other get verbs
		//[Option('b', "boards", Required = true, HelpText = "Get boards.")]
		//public bool GetBoards { get; set; }

		[Option("IncludeId")]
		public bool IncludeId { get; set; }
		[Option("IncludeTitle")]
		public bool IncludeTitle { get; set; }
		[Option("IncludeDescription")]
		public bool IncludeDescription { get; set; }
		[Option("IncludeCreateDate")]
		public bool IncludeCreateDate { get; set; }
		[Option("IncludeCreatorId")]
		public bool IncludeCreatorId { get; set; }
		[Option("IncludeModeratorIds")]
		public bool IncludeModeratorIds { get; set; }
		[Option("IncludeSubscriberIds")]
		public bool IncludeSubscriberIds { get; set; }
		[Option("IncludeArticleIds")]
		public bool IncludeArticleIds { get; set; }
		[Option("IncludeDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("IncludeImageId")]
		public bool IncludeImageId { get; set; }
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
