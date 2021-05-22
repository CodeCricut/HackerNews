using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetBoards
{
	public interface IBoardInclusionOptions
	{
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
	}
}
