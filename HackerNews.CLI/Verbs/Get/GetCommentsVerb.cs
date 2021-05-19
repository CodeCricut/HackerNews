using CommandLine;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	[Verb("get-c", HelpText = "Get comments from the database.")]
	public class GetCommentsVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		//[Option('c', "comments", Required = true, HelpText = "Get comments.")]
		//public bool GetComments { get; set; }

		[Option("IncludeId")]
		public bool IncludeId { get; set; }
		[Option("IncludeUserId")]
		public bool IncludeUserId { get; set; }
		[Option("IncludeText")]
		public bool IncludeText { get; set; }
		[Option("IncludeUrl")]
		public bool IncludeUrl { get; set; }
		[Option("IncludeKarma")]
		public bool IncludeKarma { get; set; }
		[Option("IncludeCommentIds")]
		public bool IncludeCommentIds { get; set; }
		[Option("IncludeParentCommentId")]
		public bool IncludeParentCommentId { get; set; }
		[Option("IncludeParentArticleId")]
		public bool IncludeParentArticleId { get; set; }
		[Option("IncludeDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("IncludeUsersLiked")]
		public bool IncludeUsersLiked { get; set; }
		[Option("IncludeUsersDisliked")]
		public bool IncludeUsersDisliked { get; set; }
		[Option("IncludePostDate")]
		public bool IncludePostDate { get; set; }
		[Option("IncludeBoardId")]
		public bool IncludeBoardId { get; set; }
	}

	public class GetCommentsVerb : IHostedService
	{
		private readonly GetCommentsVerbOptions _options;
		private readonly IGetCommentProcessor _getCommentProcessor;

		public GetCommentsVerb(GetCommentsVerbOptions options,
			IGetCommentProcessor getCommentProcessor)
		{
			_options = options;
			_getCommentProcessor = getCommentProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _getCommentProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
