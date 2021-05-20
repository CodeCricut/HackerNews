using CommandLine;

namespace HackerNews.CLI.Verbs.Post
{
	public interface IPostEntityOptions
	{
		[Option('u', "username", Required = true, HelpText = "The username to login with.")]
		public string Username { get; set; }

		[Option('p', "password", Required = true, HelpText = "The password to login with.")]
		public string Password { get; set; }
	}

	public class PostEntityOptions : IPostEntityOptions
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
