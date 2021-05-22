using CommandLine;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Options
{
	[Verb("get-board")]
	public class GetBoardByIdOptions :
		IBoardInclusionOptions,
		IVerbosityOptions,
		IPrintOptions,
		IFileOptions,
		IIdOptions
	{
		[Option("includeId")]
		public bool IncludeId { get; set; }
		[Option("includeTitle")]
		public bool IncludeTitle { get; set; }
		[Option("includeDescription")]
		public bool IncludeDescription { get; set; }
		[Option("includeCreateDate")]
		public bool IncludeCreateDate { get; set; }
		[Option("includeCreatorId")]
		public bool IncludeCreatorId { get; set; }
		[Option("includeModeratorIds")]
		public bool IncludeModeratorIds { get; set; }
		[Option("includeSubscriberIds")]
		public bool IncludeSubscriberIds { get; set; }
		[Option("includeArticleIds")]
		public bool IncludeArticleIds { get; set; }
		[Option("includeDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("includeImageId")]
		public bool IncludeImageId { get; set; }

		[Option('p', "print", HelpText = "Print the entities to the console")]
		public bool Print { get; set; }

		[Option('f', "file", HelpText = "The location of a file, which if specified, the entities will be written to")]
		public string FileLocation { get; set; }

		[Option("id", Required = true, HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }

		[Option("all", HelpText = "Output all fields of the retrieved entities")]
		public bool IncludeAllFields { get; set; }

		[Option('v', "verbose", HelpText = "Output the most information about what is happening.")]
		public bool Verbose { get; set; }
	}
}
