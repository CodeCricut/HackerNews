using CommandLine;

namespace HackerNews.CLI.Verbs.Post
{
	public interface IPostEntityVerbOptions
	{
		[Option('u', "username", Required = true, HelpText = "The username to login with.")]
		public string Username { get; set; }

		[Option('p', "password", Required = true, HelpText = "The password to login with.")]
		public string Password { get; set; }
	}

	public class PostEntityVerbOptions : IPostEntityVerbOptions
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
