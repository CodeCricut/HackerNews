using CommandLine;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	[Verb("get-a", HelpText = "Get articles from the database.")]
	public class GetArticlesVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		//// Unique id for this verb
		//[Option('a', "articles", Required = true, HelpText = "Get articles.")]
		//public bool GetArticles { get; set; }

		[Option("IncludeId")]
		public bool IncludeId { get; set; }
		[Option("IncludeType")]
		public bool IncludeType { get; set; }
		[Option("IncludeUserId")]
		public bool IncludeUserId { get; set; }
		[Option("IncludeText")]
		public bool IncludeText { get; set; }
		[Option("IncludeCommentIds")]
		public bool IncludeCommentIds { get; set; }
		[Option("IncludeKarma")]
		public bool IncludeKarma { get; set; }
		[Option("IncludeTitle")]
		public bool IncludeTitle { get; set; }
		[Option("IncludeUsersLiked")]
		public bool IncludeUsersLiked { get; set; }
		[Option("IncludeUsersDisliked")]
		public bool IncludeUsersDisliked { get; set; }
		[Option("IncludePostDate")]
		public bool IncludePostDate { get; set; }
		[Option("IncludeBoardId")]
		public bool IncludeBoardId { get; set; }
		[Option("IncludeDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("IncludeAssociatedImageId")]
		public bool IncludeAssociatedImageId { get; set; }
	}


	public class GetArticlesVerb : IHostedService
	{
		private readonly GetArticlesVerbOptions _options;
		private readonly IGetArticleProcessor _getArticleProcessor;

		public GetArticlesVerb(GetArticlesVerbOptions options,
			IGetArticleProcessor getArticleProcessor)
		{
			_options = options;
			_getArticleProcessor = getArticleProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _getArticleProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
