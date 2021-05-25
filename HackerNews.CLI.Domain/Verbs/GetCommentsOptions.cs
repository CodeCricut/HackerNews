using CommandLine;
using HackerNews.CLI.Options;
using HackerNews.CLI.Options.Verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Verbs
{
	[Verb("get-comments")]
	public class GetCommentsOptions :
		IVerbOptions,
		ICommentInclusionOptions,
		IGetEntitiesOptions
	{
		public bool Verbose { get; set; }
		public bool Print { get; set; }
		public string FileLocation { get; set; }
		public IEnumerable<int> Ids { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	
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
		public bool IncludeAllFields { get; set; }
	}
}
