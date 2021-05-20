using CommandLine;
using HackerNews.CLI.Verbs.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.PostArticle
{
	[Verb("post-a", HelpText = "Post an article")]
	public class PostArticleVerbOptions : PostEntityVerbOptions, IPostEntityVerbOptions
	{
		[Option("Type", HelpText = "The type to assign to the article being posted (news, opinion, meta, question)")]
		public string Type { get; set; }
		[Option("Title", HelpText = "The title to assign to the article being posted")]
		public string Title { get; set; }
		[Option("Text", HelpText = "The text to assign to the article being posted")]
		public string Text { get; set; }
		[Option("BoardId", HelpText = "The id of the parent board to assign to the article being posted")]
		public int BoardId { get; set; }
	}

}
