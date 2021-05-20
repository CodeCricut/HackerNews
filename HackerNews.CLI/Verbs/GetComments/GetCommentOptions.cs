using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetComments
{
	[Verb("get-c", HelpText = "Get comments from the database.")]
	public class GetCommentsOptions : GetEntityOptions, IGetEntityOptions
	{
		[Option("IncludeId")]
		public bool IncludeId { get; set; }
		[Option("IncludeUserId")]
		public bool IncludeUserId { get; set; }
		[Option("IncludeText")]
		public bool IncludeText { get; set; }
		[Option("IncludeUrl")]
		public bool IncludeUrl { get; set; }
		[Option("IncludeKarma")]
		public bool IncludeKarma { get; set; }
		[Option("IncludeCommentIds")]
		public bool IncludeCommentIds { get; set; }
		[Option("IncludeParentCommentId")]
		public bool IncludeParentCommentId { get; set; }
		[Option("IncludeParentArticleId")]
		public bool IncludeParentArticleId { get; set; }
		[Option("IncludeDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("IncludeUsersLiked")]
		public bool IncludeUsersLiked { get; set; }
		[Option("IncludeUsersDisliked")]
		public bool IncludeUsersDisliked { get; set; }
		[Option("IncludePostDate")]
		public bool IncludePostDate { get; set; }
		[Option("IncludeBoardId")]
		public bool IncludeBoardId { get; set; }
	}
}
