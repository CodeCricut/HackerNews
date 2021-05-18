using HackerNews.CLI.Verbs.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Util
{
	// TODO: check that all required args are included to return true from any method
	public static class PostVerbOptionsExtensions
	{
		public static bool IsBoardType(this PostVerbOptions options)
		{
			return ! string.IsNullOrEmpty(options.BoardTitle);
		}

		public static bool IsArticleType(this PostVerbOptions options)
		{
			return !string.IsNullOrEmpty(options.ArticleTitle);
		}

		public static bool IsCommentType(this PostVerbOptions options)
		{
			return !string.IsNullOrEmpty(options.CommentText);
		}

		public static bool IsPublicUserType(this PostVerbOptions options)
		{
			// TODO:
			return false;
		}
	}
}
