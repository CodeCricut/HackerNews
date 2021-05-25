using CommandLine;

namespace HackerNews.CLI.Requests.Configuration
{
	public interface IPostBoardOptions
	{
		[Option('t', "title", Required = true, HelpText = "The title to assign to the board being posted")]
		public string Title { get; set; }
		[Option('d', "description", Required = true, HelpText = "The description to assign to the board being posted")]
		public string Description { get; set; }
	}
}
