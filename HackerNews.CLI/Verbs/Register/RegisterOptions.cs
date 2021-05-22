using CommandLine;

namespace HackerNews.CLI.Verbs.Register
{
	[Verb("register", HelpText = "Register as a new user, and print the returned JWT if successful.")]
	public class RegisterOptions
	{
		[Option('u', "username", Required = true, HelpText = "The username to register with.")]
		public string Username { get; set; }

		[Option('p', "password", Required = true, HelpText = "The password to register with.")]
		public string Password { get; set; }

		[Option('f', "firstname", Required = true, HelpText = "The first name to register with.")]
		public string Firstname { get; set; }

		[Option('l', "lastname", Required = true, HelpText = "The last name to register with.")]
		public string Lastname { get; set; }
	}
}
