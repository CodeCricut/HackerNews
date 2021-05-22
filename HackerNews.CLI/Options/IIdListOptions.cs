using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IIdListOptions
	{
		[Option("ids")]
		public IEnumerable<int> Ids { get; set; }
	}
}
