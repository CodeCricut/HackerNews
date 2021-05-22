using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IPrintOptions
	{
		[Option('p', "print", HelpText = "Print the entities to the console")]
		public bool Print { get; set; }
	}
}
