﻿using CommandLine;
using System.Collections.Generic;

namespace HackerNews.CLI.Verbs.Get
{
	public interface IGetEntityVerbOptions
	{
		public bool Print { get; set; }
		public string File { get; set; }
		public int Id { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public IEnumerable<int> Ids { get; set; }
		public bool IncludeAllFields { get; set; }
	}

	[Verb("get", HelpText = "Retrieve data from the server, typically with the GET http verb.")]
	public class GetEntityVerbOptions :
		IGetEntityVerbOptions
	{
		[Option('p', "print", HelpText = "Print the entities to the console")]
		public bool Print { get; set; }

		[Option('f', "file", HelpText = "The location of a file, which if specified, the entities will be written to")]
		public string File { get; set; }

		[Option("id", HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }

		[Option('n', "page-number", HelpText = "The page number of entities to retrievw.")]
		public int PageNumber { get; set; }

		[Option('s', "page-size", HelpText = "The page size of entities to retrieve.")]
		public int PageSize { get; set; }

		[Option("ids", HelpText = "The IDs of the entity to be gotten.")]
		public IEnumerable<int> Ids { get; set; }

		[Option("includeAllFields", HelpText = "Output all fields of the retrieved boards")]
		public bool IncludeAllFields { get; set; }
	}
}