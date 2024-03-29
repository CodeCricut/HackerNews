﻿using CommandLine;
using HackerNews.CLI.Options;
using HackerNews.CLI.Options.Verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Verbs
{
	[Verb("get-user")]
	public class GetPublicUserByIdOptions :
		IVerbOptions,
		IGetEntityByIdOptions,
		IPublicUserInclusionOptions
	{
		public bool Verbose { get; set; }
		public bool Print { get; set; }
		public string FileLocation { get; set; }
		public int Id { get; set; }
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
