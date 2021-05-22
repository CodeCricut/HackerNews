using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Verbs.GetArticles
{
	[Verb("get-a", HelpText = "Get articles from the database.")]
	public class GetArticleOptions : GetEntityOptions, IGetEntityOptions, IArticleInclusionOptions
	{
		[Option("IncludeId")]
		public bool IncludeId { get; set; }
		[Option("IncludeType")]
		public bool IncludeType { get; set; }
		[Option("IncludeUserId")]
		public bool IncludeUserId { get; set; }
		[Option("IncludeText")]
		public bool IncludeText { get; set; }
		[Option("IncludeCommentIds")]
		public bool IncludeCommentIds { get; set; }
		[Option("IncludeKarma")]
		public bool IncludeKarma { get; set; }
		[Option("IncludeTitle")]
		public bool IncludeTitle { get; set; }
		[Option("IncludeUsersLiked")]
		public bool IncludeUsersLiked { get; set; }
		[Option("IncludeUsersDisliked")]
		public bool IncludeUsersDisliked { get; set; }
		[Option("IncludePostDate")]
		public bool IncludePostDate { get; set; }
		[Option("IncludeBoardId")]
		public bool IncludeBoardId { get; set; }
		[Option("IncludeDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("IncludeAssociatedImageId")]
		public bool IncludeAssociatedImageId { get; set; }
	}
}
