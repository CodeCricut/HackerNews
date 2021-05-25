namespace HackerNews.CLI.InclusionConfiguration
{
	public class BoardInclusionConfiguration
	{
		public BoardInclusionConfiguration(bool includeAll = false)
		{
			if (includeAll)
			{
				IncludeId = true;
				IncludeTitle = true;
				IncludeDescription = true;
				IncludeCreateDate = true;
				IncludeCreatorId = true;
				IncludeModeratorIds = true;
				IncludeSubscriberIds = true;
				IncludeArticleIds = true;
				IncludeDeleted = true;
				IncludeBoardImageId = true;
			}
		}

		public bool IncludeId { get; set; }
		public bool IncludeTitle { get; set; }
		public bool IncludeDescription { get; set; }
		public bool IncludeCreateDate { get; set; }
		public bool IncludeCreatorId { get; set; }
		public bool IncludeModeratorIds { get; set; }
		public bool IncludeSubscriberIds { get; set; }
		public bool IncludeArticleIds { get; set; }
		public bool IncludeDeleted { get; set; }
		public bool IncludeBoardImageId { get; set; }
	}
}
