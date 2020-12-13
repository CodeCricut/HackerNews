using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Articles
{
	public class ArticleDetailsViewModel
	{
		public GetArticleModel Article { get; set; }
		public FrontendPage<GetCommentModel> CommentPage { get; set; }

		public GetBoardModel Board { get; set; }
		public PostCommentModel PostCommentModel { get; set; }

		public GetPublicUserModel User { get; set; }
		public bool LoggedIn { get; set; }
		public bool UserSavedArticle { get; set; }

		public bool UserWroteArticle { get; set; }

		public string AssociatedImageDataUrl { get; set; }
	}
}
