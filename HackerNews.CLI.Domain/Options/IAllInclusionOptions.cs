using CommandLine;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IAllInclusionOptions
	{
		[Option("all", HelpText = "Output all fields of the retrieved entities")]
		public bool IncludeAllFields { get; set; }
	}
}
