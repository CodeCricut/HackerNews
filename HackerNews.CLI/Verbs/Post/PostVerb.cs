using CommandLine;
using HackerNews.CLI.Util;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Post
{
	interface IPostBoardOptions
	{
		[Option("boardTitle", SetName = "boards", HelpText = "The title to assign to the board being posted")]
		public string BoardTitle { get; set; }
		[Option("boardDescription", SetName = "boards", HelpText = "The description to assign to the board being posted")]
		public string BoardDescription { get; set; }
	}

	interface IPostArticleOptions
	{
		[Option("articleType", SetName = "articles", HelpText = "The type to assign to the article being posted (news, opinion, meta, question)")]
		public string ArticleType { get; set; }
		[Option("articleTitle", SetName = "articles", HelpText = "The title to assign to the article being posted")]
		public string ArticleTitle { get; set; }
		[Option("articleText", SetName = "articles", HelpText = "The text to assign to the article being posted")]
		public string ArticleText { get; set; }
		[Option("articleBoardId", SetName = "articles", HelpText = "The id of the parent board to assign to the article being posted")]
		public int ArticleBoardId { get; set; }
	} 

	[Verb("post", HelpText = "Post an entity to the server.")]
	public class PostVerbOptions : 
		IPostBoardOptions,
		IPostArticleOptions
	{
		[Option('u', "username", Required = true, HelpText = "The username to login with.")]
		public string Username { get; set; }

		[Option('p', "password", Required = true, HelpText = "The password to login with.")]
		public string Password { get; set; }

		// Boards
		public string BoardTitle { get; set; }
		public string BoardDescription { get; set; }

		// Articles
		public string ArticleType { get; set; }
		public string ArticleTitle { get; set; }
		public string ArticleText { get; set; }
		public int ArticleBoardId { get; set; }
	}

	public class PostVerb : IHostedService
	{
		private readonly PostVerbOptions _options;
		private readonly ILogger<PostVerb> _logger;
		private readonly IPostBoardProcessor _postBoardProcessor;
		private readonly IPostArticleProcessor _postArticleProcessor;

		public PostVerb(PostVerbOptions options, 
			ILogger<PostVerb> logger,
			IPostBoardProcessor postBoardProcessor,
			IPostArticleProcessor postArticleProcessor)
		{
			_options = options;
			_logger = logger;
			_postBoardProcessor = postBoardProcessor;
			_postArticleProcessor = postArticleProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			if (_options.IsBoardType())
				return _postBoardProcessor.ProcessGetVerbOptionsAsync(_options);
			else if (_options.IsArticleType())
				return _postArticleProcessor.ProcessGetVerbOptionsAsync(_options);
			throw new Exception();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
