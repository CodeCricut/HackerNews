using CommandLine;
using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Verbs.GetBoards;

namespace HackerNews.CLI.Options
{
	[Verb("get-board")]
	public class GetBoardByIdOptions :
		IVerbOptions,
		IBoardInclusionOptions,
		IGetEntityByIdOptions
	{
		public bool IncludeAllFields { get; set; }

		public bool IncludeId { get; set; }
		public bool IncludeTitle { get; set; }
		public bool IncludeDescription { get; set; }
		public bool IncludeCreateDate { get; set; }
		public bool IncludeCreatorId { get; set; }
		public bool IncludeModeratorIds { get; set; }
		public bool IncludeSubscriberIds { get; set; }
		public bool IncludeArticleIds { get; set; }
		public bool IncludeDeleted { get; set; }
		public bool IncludeImageId { get; set; }

		public bool Verbose { get; set; }

		public bool Print { get; set; }

		public string FileLocation { get; set; }

		public int Id { get; set; }
	}
}
