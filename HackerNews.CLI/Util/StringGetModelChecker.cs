using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.CLI.Util
{
	public static class StringGetModelChecker
	{
		public static bool IsBoardType(this string s)
		{
			return s.ConvertToGetModelType() == typeof(GetBoardModel);
		}

		public static bool IsArticleType(this string s)
		{
			return s.ConvertToGetModelType() == typeof(GetArticleModel);
		}

		public static bool IsCommentType(this string s)
		{
			return s.ConvertToGetModelType() == typeof(GetCommentModel);
		}

		public static bool IsPublicUserType(this string s)
		{
			return s.ConvertToGetModelType() == typeof(GetPublicUserModel);
		}
	}
}
