using CommandLine;
using HackerNews.ApiConsumer.Account;
using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.CLI.Services;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.ProgramServices
{
	[Verb("get", HelpText = "Retrieve data from the server, typically with the GET http verb.")]
	public class GetVerbOptions
	{
		[Option('t', "type", Required = true, HelpText = "The type of entity to retrieve (board, article, comment, user).")]
		public string Type { get; set; }

		[Option("id", HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }

		[Option('n', "page-number", HelpText = "The page number of entities to retrievw.")]
		public int PageNumber { get; set; }

		[Option('s', "page-size", HelpText = "The page size of entities to retrieve.")]
		public int PageSize { get; set; }

		[Option("ids", HelpText = "The IDs of the entity to be gotten.")]
		public IEnumerable<int> Ids { get; set; }
	}

	public class GetVerb : IHostedService, IDisposable
	{
		private readonly ILogger<GetVerb> _logger;
		private readonly GetVerbOptions _options;
		private readonly GetVerbProcessor<GetBoardModel> _boardVerbProcessor;
		private readonly GetVerbProcessor<GetArticleModel> _articleVerbProcessor;
		private readonly GetVerbProcessor<GetCommentModel> _commentVerbProcessor;

		public GetVerb(
			GetVerbOptions options,
			ILogger<GetVerb> logger,
			GetVerbProcessor<GetBoardModel> boardVerbProcessor,
			GetVerbProcessor<GetArticleModel> articleVerbProcessor,
			GetVerbProcessor<GetCommentModel> commentVerbProcessor)
		{
			_options = options;
			_logger = logger;
			_boardVerbProcessor = boardVerbProcessor;
			_articleVerbProcessor = articleVerbProcessor;
			_commentVerbProcessor = commentVerbProcessor;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			if (_options.Type.IsBoardType())
				await _boardVerbProcessor.ProcessGetVerbOptionsAsync(_options);
			else if (_options.Type.IsArticleType())
				await _articleVerbProcessor.ProcessGetVerbOptionsAsync(_options); 
			else if (_options.Type.IsCommentType())
				await _commentVerbProcessor.ProcessGetVerbOptionsAsync(_options); // TODO
			/*
			else if (_options.Type.IsUserType())
				await _userTypeVerbProcessor.ProcessGetVerbOptionsAsync(_options); // TODO
			*/
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		public void Dispose()
		{
		}
	}
}
