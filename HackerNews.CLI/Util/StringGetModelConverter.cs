using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Util
{
	public static class StringGetModelConverter  // : IStringGetModelConverter
	{
		/// <returns>The matching type based on the provided string. Ex "article" returns <see cref="HackerNews.Domain.Common.Models.Articles.GetArticleModel"./></returns>

		public static Type ConvertToGetModelType(this string s)
		{
			if (s == null) throw new ArgumentNullException();

			string normalString = s.Trim().ToLower();
			if ("board".Equals(normalString)) return typeof(GetBoardModel);
			if ("article".Equals(normalString)) return typeof(GetArticleModel);
			if ("comment".Equals(normalString)) return typeof(GetCommentModel);
			if ("user".Equals(normalString)) return typeof(GetPublicUserModel);

			throw new ArgumentException();
		}
	}
}
