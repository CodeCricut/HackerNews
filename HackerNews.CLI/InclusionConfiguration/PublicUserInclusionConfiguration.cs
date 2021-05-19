namespace HackerNews.CLI.InclusionConfiguration
{
	public class PublicUserInclusionConfiguration
	{
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
