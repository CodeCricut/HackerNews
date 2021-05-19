namespace HackerNews.CLI.InclusionConfiguration
{
	public class CommentInclusionConfiguration
	{
		public bool IncludeId { get; set; }
		public bool IncludeUserId { get; set; }
		public bool IncludeText { get; set; }
		public bool IncludeUrl { get; set; }
		public bool IncludeKarma { get; set; }
		public bool IncludeCommentIds { get; set; }
		public bool IncludeParentCommentId { get; set; }
		public bool IncludeParentArticleId { get; set; }
		public bool IncludeDeleted { get; set; }
		public bool IncludeUsersLiked { get; set; }
		public bool IncludeUsersDisliked { get; set; }
		public bool IncludePostDate { get; set; }
		public bool IncludeBoardId { get; set; }
	}
}
