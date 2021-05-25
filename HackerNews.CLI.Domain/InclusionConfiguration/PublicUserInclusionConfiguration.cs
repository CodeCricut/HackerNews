namespace HackerNews.CLI.InclusionConfiguration
{
	public class PublicUserInclusionConfiguration
	{
		public PublicUserInclusionConfiguration(bool includeAll = true)
		{
			if (includeAll)
			{
				IncludeId = true;
				IncludeUsername = true;
				IncludeKarma = true;
				IncludeArticleIds = true;
				IncludeCommentIds = true;
				IncludeJoinDate = true;
				IncludeDeleted = true;
				IncludeProfileImageId = true;
			}
		}

		public bool IncludeId { get; set; }
		public bool IncludeUsername { get; set; }
		public bool IncludeKarma { get; set; }
		public bool IncludeArticleIds { get; set; }
		public bool IncludeCommentIds { get; set; }
		public bool IncludeJoinDate { get; set; }
		public bool IncludeDeleted { get; set; }
		public bool IncludeProfileImageId { get; set; }
	}
}
