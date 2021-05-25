using CommandLine;
using System.Collections.Generic;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IIdListOptions
	{
		[Option("ids")]
		public IEnumerable<int> Ids { get; set; }
	}
}
