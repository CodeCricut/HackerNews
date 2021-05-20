using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetBoards
{
	[Verb("get-b", HelpText = "Get boards from the database.")]
	public class GetBoardsOptions : GetEntityOptions, IGetEntityOptions
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
	}

}
