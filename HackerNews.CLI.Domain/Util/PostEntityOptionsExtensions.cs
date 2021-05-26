using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.Requests.Configuration;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;

namespace HackerNews.CLI.Util
{
	public static class PostEntityOptionsExtensions
	{
		public static PostBoardModel ToPostModel(this IPostBoardOptions options)
		{
			return new PostBoardModel()
			{
				Title = options.Title,
				Description = options.Description
			};
		}

		public static PostArticleModel ToPostModel(this IPostArticleOptions options)
		{
			return new PostArticleModel()
			{
				Title = options.Title,
				Text = options.Text,
				Type = options.Type,
				BoardId = options.BoardId
			};
		}

		public static PostCommentModel ToPostModel(this IPostCommentOptions options)
		{
			return new PostCommentModel()
			{
				Text = options.Text,
				ParentArticleId = options.ArticleId,
				ParentCommentId = options.CommentId
			};
		}
	}
}
