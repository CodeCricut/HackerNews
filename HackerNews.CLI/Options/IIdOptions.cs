using CommandLine;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IIdOptions
	{
		[Option("id", Required = true, HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }
	}
}
