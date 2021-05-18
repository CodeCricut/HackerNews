using CommandLine;
using HackerNews.CLI.Util;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	interface IGetBoardVerbOptions
	{
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

	[Verb("get", HelpText = "Retrieve data from the server, typically with the GET http verb.")]
	public class GetVerbOptions : IGetBoardVerbOptions
	{
		[Option('t', "type", Required = true, HelpText = "The type of entity to retrieve (board, article, comment, user).")]
		public string Type { get; set; }

		[Option('p', "print", HelpText = "Print the entities to the console")]
		public bool Print { get; set; }

		[Option('f', "file",  HelpText = "The location of a file, which if specified, the entities will be written to")]
		public string File { get; set; }

		[Option("id", HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }

		[Option('n', "page-number", HelpText = "The page number of entities to retrievw.")]
		public int PageNumber { get; set; }

		[Option('s', "page-size", HelpText = "The page size of entities to retrieve.")]
		public int PageSize { get; set; }

		[Option("ids", HelpText = "The IDs of the entity to be gotten.")]
		public IEnumerable<int> Ids { get; set; }

		[Option("includeAllFields", HelpText = "Output all fields of the retrieved boards")]
		public bool IncludeAllFields { get; set; }

		// Boards
		public bool IncludeBoardId { get; set; }
		public bool IncludeBoardTitle { get; set; }
		public bool IncludeBoardDescription { get; set; }
		public bool IncludeBoardCreateDate { get; set; }
		public bool IncludeBoardCreatorId { get; set; }
		public bool IncludeBoardModeratorIds { get; set; }
		public bool IncludeBoardSubscriberIds { get; set; }
		public bool IncludeBoardArticleIds { get; set; }
		public bool IncludeBoardDeleted { get; set; }
		public bool IncludeBoardImageId { get; set; }
	}

	public class GetVerb : IHostedService
	{
		private readonly ILogger<GetVerb> _logger;
		private readonly GetVerbOptions _options;
		private readonly IGetBoardProcessor _boardVerbProcessor;
		private readonly IGetArticleProcessor _articleVerbProcessor;
		private readonly IGetCommentProcessor _commentVerbProcessor;
		private readonly IGetPublicUserProcessor _userVerbProcessor;

		public GetVerb(
			GetVerbOptions options,
			ILogger<GetVerb> logger,
			IGetBoardProcessor boardVerbProcessor,
			IGetArticleProcessor articleVerbProcessor,
			IGetCommentProcessor commentVerbProcessor,
			IGetPublicUserProcessor userVerbProcessor)
		{
			_options = options;
			_logger = logger;
			_boardVerbProcessor = boardVerbProcessor;
			_articleVerbProcessor = articleVerbProcessor;
			_commentVerbProcessor = commentVerbProcessor;
			_userVerbProcessor = userVerbProcessor;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			if (_options.Type.IsBoardType())
				await _boardVerbProcessor.ProcessGetVerbOptionsAsync(_options);
			else if (_options.Type.IsArticleType())
				await _articleVerbProcessor.ProcessGetVerbOptionsAsync(_options);
			else if (_options.Type.IsCommentType())
				await _commentVerbProcessor.ProcessGetVerbOptionsAsync(_options);
			else if (_options.Type.IsPublicUserType())
				await _userVerbProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
