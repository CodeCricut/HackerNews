using CommandLine;
using HackerNews.CLI.Verbs.Post;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.PostComment
{
	[Verb("post-c", HelpText = "Post a comment.")]
	public class PostCommentVerbOptions : PostEntityVerbOptions, IPostEntityVerbOptions
	{
		[Option("Text", SetName = "comments", HelpText = "The text to assign to the comment being posted")]
		public string Text { get; set; }
		[Option("ArticleId", SetName = "comments", HelpText = "The parent article id to which to assign to the comment being posted")]
		public int ArticleId { get; set; }
		[Option("CommentId", SetName = "comments", HelpText = "The parent comment id to which to assign to the comment being posted")]
		public int CommentId { get; set; }
	}

	public class PostCommentVerb : IHostedService
	{
		private readonly PostCommentVerbOptions _options;
		private readonly IPostCommentProcessor _postCommentProcessor;

		public PostCommentVerb(PostCommentVerbOptions options,
			IPostCommentProcessor postCommentProcessor)
		{
			_options = options;
			_postCommentProcessor = postCommentProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _postCommentProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
