using CommandLine;
using HackerNews.CLI.Options;
using HackerNews.CLI.Options.Verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Verbs
{
	[Verb("get-users")]
	public class GetPublicUsersOptions :
		IVerbOptions,
		IPublicUserInclusionOptions,
		IGetEntitiesOptions
	{
		public bool Verbose { get; set; }
		public bool Print { get; set; }
		public string FileLocation { get; set; }
		public IEnumerable<int> Ids { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public bool IncludeId { get; set; }
		public bool IncludeUsername { get; set; }
		public bool IncludeKarma { get; set; }
		public bool IncludeArticleIds { get; set; }
		public bool IncludeCommentIds { get; set; }
		public bool IncludeJoinDate { get; set; }
		public bool IncludeDeleted { get; set; }
		public bool IncludeProfileImageId { get; set; }
		public bool IncludeAllFields { get; set; }
	}
}
