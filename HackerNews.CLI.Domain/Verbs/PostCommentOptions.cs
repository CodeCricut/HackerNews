using CommandLine;
using HackerNews.CLI.Domain.Options;
using HackerNews.CLI.Options.Verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Verbs
{
	[Verb("post-comment")]
	public class PostCommentOptions :
		IVerbOptions,
		IPostCommentOptions,
		IPostEntityOptions
	{
		public string Text { get; set; }
		public int ArticleId { get; set; }
		public int CommentId { get; set; }
		
		public string Username { get; set; }
		public string Password { get; set; }
		
		public bool Verbose { get; set; }
		
		public bool Print { get; set; }
		
		public string FileLocation { get; set; }
	}
}
