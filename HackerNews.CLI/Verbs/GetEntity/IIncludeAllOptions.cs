using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IIncludeAllOptions
	{
		public bool IncludeAllFields { get; set; }
	}
}
