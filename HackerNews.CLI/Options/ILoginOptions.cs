using CommandLine;

namespace HackerNews.CLI.Options
{
	public interface ILoginOptions
	{
		[Option("username", Required = true, HelpText = "The username to login with.")]
		public string Username { get; set; }

		[Option("password", Required = true, HelpText = "The password to login with.")]
		public string Password { get; set; }
	}
}
