using CommandLine;

namespace HackerNews.CLI.Domain.Verbs
{
	public interface IPostCommentOptions
	{
		[Option("text", Required = true, HelpText = "The text to assign to the comment being posted")]
		string Text { get; set; }
		[Option("articleId", HelpText = "The parent article id to which to assign to the comment being posted")]
		int ArticleId { get; set; }
		[Option("commentId", HelpText = "The parent comment id to which to assign to the comment being posted")]
		int CommentId { get; set; }
	}
}