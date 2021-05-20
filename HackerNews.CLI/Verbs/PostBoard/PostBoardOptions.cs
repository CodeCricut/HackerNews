using CommandLine;
using HackerNews.CLI.Verbs.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.PostBoard
{
	[Verb("post-b", HelpText = "Post a board.")]
	public class PostBoardOptions : PostEntityOptions, IPostEntityOptions
	{
		[Option("Title", SetName = "boards", HelpText = "The title to assign to the board being posted")]
		public string Title { get; set; }
		[Option("Description", SetName = "boards", HelpText = "The description to assign to the board being posted")]
		public string Description { get; set; }
	}
}
