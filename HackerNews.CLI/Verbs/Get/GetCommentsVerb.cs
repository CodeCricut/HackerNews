using CommandLine;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	[Verb("get", HelpText = "Get comments.")]
	public class GetCommentsVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		[Option('c', "comments", Required = true, HelpText = "Get comments.")]
		public bool GetComments { get; set; }

		[Option("includeCommentId", SetName = "comments")]
		public bool IncludeCommentId { get; set; }
		[Option("includeCommentUserId", SetName = "comments")]
		public bool IncludeCommentUserId { get; set; }
		[Option("includeCommentText", SetName = "comments")]
		public bool IncludeCommentText { get; set; }
		[Option("includeCommentUrl", SetName = "comments")]
		public bool IncludeCommentUrl { get; set; }
		[Option("includeCommentKarma", SetName = "comments")]
		public bool IncludeCommentKarma { get; set; }
		[Option("includeCommentCommentIds", SetName = "comments")]
		public bool IncludeCommentCommentIds { get; set; }
		[Option("includeCommentParentCommentId", SetName = "comments")]
		public bool IncludeCommentParentCommentId { get; set; }
		[Option("includeCommentParentArticleId", SetName = "comments")]
		public bool IncludeCommentParentArticleId { get; set; }
		[Option("includeCommentDeleted", SetName = "comments")]
		public bool IncludeCommentDeleted { get; set; }
		[Option("includeCommentUsersLiked", SetName = "comments")]
		public bool IncludeCommentUsersLiked { get; set; }
		[Option("includeCommentUsersDisliked", SetName = "comments")]
		public bool IncludeCommentUsersDisliked { get; set; }
		[Option("includeCommentPostDate", SetName = "comments")]
		public bool IncludeCommentPostDate { get; set; }
		[Option("includeCommentBoardId", SetName = "comments")]
		public bool IncludeCommentBoardId { get; set; }
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
