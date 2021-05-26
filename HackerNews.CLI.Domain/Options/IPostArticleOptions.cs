using CommandLine;

namespace HackerNews.CLI.Domain.Verbs
{
	public interface IPostArticleOptions
	{
		[Option("type", Required = true, HelpText = "The type to assign to the article being posted (news, opinion, meta, question)")]
		string Type { get; set; }
		[Option("title", Required = true, HelpText = "The title to assign to the article being posted")]
		string Title { get; set; }
		[Option("text", Required = true, HelpText = "The text to assign to the article being posted")]
		string Text { get; set; }
		[Option("boardId", Required = true, HelpText = "The id of the parent board to assign to the article being posted")]
		int BoardId { get; set; }
	}
}