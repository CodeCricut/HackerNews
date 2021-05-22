using CommandLine;
using HackerNews.CLI.Requests.Configuration;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Options
{
	[Verb("post-board")]
	public class PostBoardOptions :
		ILoginOptions,
		IPostBoardOptions,
		IVerbosityOptions,
		IPrintOptions,
		IFileOptions
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool Print { get; set; }
		public bool Verbose { get; set; }
		public string FileLocation { get; set; }
	}
}
