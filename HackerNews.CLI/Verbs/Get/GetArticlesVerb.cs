using CommandLine;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	[Verb("get")]
	public class GetArticlesVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		// Unique id for this verb
		[Option('a', "articles", Required = true, HelpText = "Get articles.")]
		public bool GetArticles { get; set; }

		[Option("includeArticleId", SetName = "articles")]
		public bool IncludeArticleId { get; set; }
		[Option("includeArticleType", SetName = "articles")]
		public bool IncludeArticleType { get; set; }
		[Option("includeArticleUserId", SetName = "articles")]
		public bool IncludeArticleUserId { get; set; }
		[Option("includeArticleText", SetName = "articles")]
		public bool IncludeArticleText { get; set; }
		[Option("includeArticleCommentIds", SetName = "articles")]
		public bool IncludeArticleCommentIds { get; set; }
		[Option("includeArticleKarma", SetName = "articles")]
		public bool IncludeArticleKarma { get; set; }
		[Option("includeArticleTitle", SetName = "articles")]
		public bool IncludeArticleTitle { get; set; }
		[Option("includeArticleUsersLiked", SetName = "articles")]
		public bool IncludeArticleUsersLiked { get; set; }
		[Option("includeArticleUsersDisliked", SetName = "articles")]
		public bool IncludeArticleUsersDisliked { get; set; }
		[Option("includeArticlePostDate", SetName = "articles")]
		public bool IncludeArticlePostDate { get; set; }
		[Option("includeArticleBoardId", SetName = "articles")]
		public bool IncludeArticleBoardId { get; set; }
		[Option("includeArticleDeleted", SetName = "articles")]
		public bool IncludeArticleDeleted { get; set; }
		[Option("includeArticleAssociatedImageId", SetName = "articles")]
		public bool IncludeArticleAssociatedImageId { get; set; }
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
