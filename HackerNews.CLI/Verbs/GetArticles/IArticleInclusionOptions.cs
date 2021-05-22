namespace HackerNews.CLI.Verbs.GetArticles
{
	public interface IArticleInclusionOptions
	{
		public bool IncludeId { get; set; }
		public bool IncludeType { get; set; }
		public bool IncludeUserId { get; set; }
		public bool IncludeText { get; set; }
		public bool IncludeCommentIds { get; set; }
		public bool IncludeKarma { get; set; }
		public bool IncludeTitle { get; set; }
		public bool IncludeUsersLiked { get; set; }
		public bool IncludeUsersDisliked { get; set; }
		public bool IncludePostDate { get; set; }
		public bool IncludeBoardId { get; set; }
		public bool IncludeDeleted { get; set; }
		public bool IncludeAssociatedImageId { get; set; }
	}
}
