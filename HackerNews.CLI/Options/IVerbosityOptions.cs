using CommandLine;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IVerbosityOptions
	{
		[Option('v', "verbose", HelpText = "Output the most information about what is happening.")]
		public bool Verbose { get; set; }
	}
}
