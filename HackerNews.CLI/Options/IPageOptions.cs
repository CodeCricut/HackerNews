using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IPageOptions
	{
		[Option('n', "page-number", HelpText = "The page number of entities to retrievw.")]
		public int PageNumber { get; set; }
		
		[Option('s', "page-size", HelpText = "The page size of entities to retrieve.")]
		public int PageSize { get; set; }
	}
}
