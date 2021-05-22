using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IPageOptions
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
