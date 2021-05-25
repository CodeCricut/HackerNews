using CommandLine;
using HackerNews.CLI.Options;
using HackerNews.CLI.Options.Verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Verbs
{
	[Verb("get-articles")]
	public class GetArticlesOptions :
		IVerbOptions,
		IArticleInclusionOptions,
		IGetEntitiesOptions
	{
		public bool Verbose { get; set; }
		public bool Print { get; set; }
		public string FileLocation { get; set; }
		public IEnumerable<int> Ids { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

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
		public bool IncludeAllFields { get; set; }
	}
}
