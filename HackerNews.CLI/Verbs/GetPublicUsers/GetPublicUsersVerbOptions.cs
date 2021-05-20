﻿using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetPublicUsers
{
	[Verb("get-u", HelpText = "Get users from the database.")]
	public class GetPublicUsersVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		[Option("IncludeId")]
		public bool IncludeId { get; set; }
		[Option("IncludeUsername")]
		public bool IncludeUsername { get; set; }
		[Option("IncludeKarma")]
		public bool IncludeKarma { get; set; }
		[Option("IncludeArticleIds")]
		public bool IncludeArticleIds { get; set; }
		[Option("IncludeCommentIds")]
		public bool IncludeCommentIds { get; set; }
		[Option("includeUserJoinDate")]
		public bool IncludeJoinDate { get; set; }
		[Option("includeUserDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("includeUserProfileImageId")]
		public bool IncludeProfileImageId { get; set; }
	}

}
