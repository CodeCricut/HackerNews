using CommandLine;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetBoardById
{
	[Verb("get-board")]
	public class GetBoardByIdOptions : 
		IBoardInclusionOptions,
		IIncludeAllOptions,
		IVerbosityOptions,
		IPrintOptions,
		IFileOptions,
		IIdOptions
	{
		[Option("IncludeId")]
		public bool IncludeId { get; set; }
		[Option("IncludeTitle")]
		public bool IncludeTitle { get; set; }
		[Option("IncludeDescription")]
		public bool IncludeDescription { get; set; }
		[Option("IncludeCreateDate")]
		public bool IncludeCreateDate { get; set; }
		[Option("IncludeCreatorId")]
		public bool IncludeCreatorId { get; set; }
		[Option("IncludeModeratorIds")]
		public bool IncludeModeratorIds { get; set; }
		[Option("IncludeSubscriberIds")]
		public bool IncludeSubscriberIds { get; set; }
		[Option("IncludeArticleIds")]
		public bool IncludeArticleIds { get; set; }
		[Option("IncludeDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("IncludeImageId")]
		public bool IncludeImageId { get; set; }

		[Option('p', "print", HelpText = "Print the entities to the console")]
		public bool Print { get; set; }

		[Option('f', "file", HelpText = "The location of a file, which if specified, the entities will be written to")]
		public string FileLocation { get; set; }

		[Option("id", HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }

		[Option('n', "page-number", HelpText = "The page number of entities to retrievw.")]
		public int PageNumber { get; set; }

		[Option('s', "page-size", HelpText = "The page size of entities to retrieve.")]
		public int PageSize { get; set; }

		[Option("ids", HelpText = "The IDs of the entity to be gotten.")]
		public IEnumerable<int> Ids { get; set; }

		[Option("all", HelpText = "Output all fields of the retrieved entities")]
		public bool IncludeAllFields { get; set; }

		[Option('v', "verbose", HelpText = "Output the most information about what is happening.")]
		public bool Verbose { get; set; }
	}
}
