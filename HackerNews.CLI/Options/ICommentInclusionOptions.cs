using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Options
{
	public interface ICommentInclusionOptions : IAllInclusionOptions
	{
		[Option("includeId")]
		public bool IncludeId { get; set; }
		[Option("includeUserId")]
		public bool IncludeUserId { get; set; }
		[Option("includeText")]
		public bool IncludeText { get; set; }
		[Option("includeUrl")]
		public bool IncludeUrl { get; set; }
		[Option("includeKarma")]
		public bool IncludeKarma { get; set; }
		[Option("includeCommentIds")]
		public bool IncludeCommentIds { get; set; }
		[Option("includeParentCommentId")]
		public bool IncludeParentCommentId { get; set; }
		[Option("includeParentArticleId")]
		public bool IncludeParentArticleId { get; set; }
		[Option("includeDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("includeUsersLiked")]
		public bool IncludeUsersLiked { get; set; }
		[Option("includeUsersDisliked")]
		public bool IncludeUsersDisliked { get; set; }
		[Option("includePostDate")]
		public bool IncludePostDate { get; set; }
		[Option("includeBoardId")]
		public bool IncludeBoardId { get; set; }
	}
}
