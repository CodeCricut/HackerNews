using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Options
{
	public interface IPublicUserInclusionOptions : IAllInclusionOptions
	{
		[Option("includeId")]
		public bool IncludeId { get; set; }

		[Option("includeUsername")]
		public bool IncludeUsername { get; set; }
		
		[Option("includeKarma")]
		public bool IncludeKarma { get; set; }
		
		[Option("includeArticleIds")]
		public bool IncludeArticleIds { get; set; }
		
		[Option("includeCommentIds")]
		public bool IncludeCommentIds { get; set; }
		
		[Option("includeJoinDate")]
		public bool IncludeJoinDate { get; set; }
		
		[Option("includeDeleted")]
		public bool IncludeDeleted { get; set; }
		
		[Option("includeProfileImageId")]
		public bool IncludeProfileImageId { get; set; }
	}
}
