using CommandLine;
using HackerNews.CLI.Domain.Options;
using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Verbs
{
	[Verb("register")]
	public class RegisterOptions :
		IVerbosityOptions,
		IVerbOptions,
		IRegisterOptions
	{
		public bool Verbose { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
	}
}
