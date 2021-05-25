using CommandLine;

namespace HackerNews.CLI.Domain.Verbs
{
	public interface IPostArticleOptions
	{
		[Option("Type", HelpText = "The type to assign to the article being posted (news, opinion, meta, question)")]
		string Type { get; set; }
		[Option("Title", HelpText = "The title to assign to the article being posted")]
		string Title { get; set; }
		[Option("Text", HelpText = "The text to assign to the article being posted")]
		string Text { get; set; }
		[Option("BoardId", HelpText = "The id of the parent board to assign to the article being posted")]
		int BoardId { get; set; }
	}
}