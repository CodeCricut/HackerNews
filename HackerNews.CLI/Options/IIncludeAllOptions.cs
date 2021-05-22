using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IIncludeAllOptions
	{
		[Option("all", HelpText = "Output all fields of the retrieved entities")]
		public bool IncludeAllFields { get; set; }
	}
}
