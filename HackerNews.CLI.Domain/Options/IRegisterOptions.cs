using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Options
{
	public interface IRegisterOptions
	{
		[Option("username", Required = true, HelpText = "The username to register with.")]
		string Username { get; set; }

		[Option("password", Required = true, HelpText = "The password to register with.")]
		string Password { get; set; }

		[Option("firstname", Required = true, HelpText = "The first name to register with.")]
		string Firstname { get; set; }

		[Option("lastname", Required = true, HelpText = "The last name to register with.")]
		string Lastname { get; set; }
	}
}
