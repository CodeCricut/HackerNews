using CommandLine;
using System.Collections.Generic;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IGetEntityOptions :
		IVerbosityOptions,
		IPrintOptions,
		IFileOptions,
		IIdOptions,
		IPageOptions,
		IIdListOptions,
		IIncludeAllOptions
	{
	}

	[Verb("get", HelpText = "Retrieve data from the server, typically with the GET http verb.")]
	public class GetEntityOptions :
		IGetEntityOptions
	{
		[Option('p', "print", HelpText = "Print the entities to the console")]
		public bool Print { get; set; }

		[Option('f', "file", HelpText = "The location of a file, which if specified, the entities will be written to")]
		public string FileLocation { get; set; }

		[Option("id", HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }

		public int PageNumber { get; set; }

		public int PageSize { get; set; }

		[Option("ids", HelpText = "The IDs of the entity to be gotten.")]
		public IEnumerable<int> Ids { get; set; }

		[Option("all", HelpText = "Output all fields of the retrieved entities")]
		public bool IncludeAllFields { get; set; }

		[Option('v', "verbose", HelpText = "Output the most information about what is happening.")]
		public bool Verbose { get; set; }
	}
}
