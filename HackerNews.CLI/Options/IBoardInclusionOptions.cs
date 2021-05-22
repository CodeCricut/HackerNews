using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Verbs.GetBoards
{
	public interface IBoardInclusionOptions : IIncludeAllOptions
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
	}
}
