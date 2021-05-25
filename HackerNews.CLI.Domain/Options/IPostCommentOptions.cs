using CommandLine;

namespace HackerNews.CLI.Domain.Verbs
{
	public interface IPostCommentOptions
	{
		[Option("Text", SetName = "comments", HelpText = "The text to assign to the comment being posted")]
		string Text { get; set; }
		[Option("ArticleId", SetName = "comments", HelpText = "The parent article id to which to assign to the comment being posted")]
		int ArticleId { get; set; }
		[Option("CommentId", SetName = "comments", HelpText = "The parent comment id to which to assign to the comment being posted")]
		int CommentId { get; set; }
	}
}