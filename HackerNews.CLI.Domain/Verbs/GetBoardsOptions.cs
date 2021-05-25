using CommandLine;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using System.Collections.Generic;

namespace HackerNews.CLI.Options
{
	[Verb("get-boards")]
	public class GetBoardsOptions :
		IVerbOptions,
		IBoardInclusionOptions,
		IGetEntitiesOptions
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

		public IEnumerable<int> Ids { get; set; }

		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
