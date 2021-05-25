using CommandLine;
using HackerNews.CLI.Domain.Options;
using HackerNews.CLI.Options.Verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Verbs
{
	[Verb("post-article")]
	public class PostArticleOptions :
		IVerbOptions,
		IPostArticleOptions,
		IPostEntityOptions
	{
		public string Type { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public int BoardId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool Verbose { get; set; }
		public bool Print { get; set; }
		public string FileLocation { get; set; }
	}
}
