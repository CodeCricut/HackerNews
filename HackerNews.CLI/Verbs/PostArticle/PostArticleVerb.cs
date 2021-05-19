using CommandLine;
using HackerNews.CLI.Verbs.Post;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.PostArticle
{
	[Verb("post-a", HelpText = "Post an article")]
	public class PostArticleVerbOptions : PostEntityVerbOptions, IPostEntityVerbOptions
	{
		[Option("Type", HelpText = "The type to assign to the article being posted (news, opinion, meta, question)")]
		public string Type { get; set; }
		[Option("Title", HelpText = "The title to assign to the article being posted")]
		public string Title { get; set; }
		[Option("Text", HelpText = "The text to assign to the article being posted")]
		public string Text { get; set; }
		[Option("BoardId", HelpText = "The id of the parent board to assign to the article being posted")]
		public int BoardId { get; set; }
	}

	public class PostArticleVerb : IHostedService
	{
		private readonly PostArticleVerbOptions _options;
		private readonly IPostArticleProcessor _postArticleProcessor;

		public PostArticleVerb(PostArticleVerbOptions options,
			IPostArticleProcessor postArticleProcessor)
		{
			_options = options;
			_postArticleProcessor = postArticleProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _postArticleProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
