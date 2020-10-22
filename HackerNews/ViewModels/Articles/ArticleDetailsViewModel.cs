using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels
{
	public class ArticleDetailsViewModel
	{
		public GetArticleModel Article { get; set; }
		public Page<GetCommentModel> CommentPage { get; set; }

		public GetBoardModel Board { get; set; }
		public PostCommentModel PostCommentModel { get; set; }

		public GetPublicUserModel User { get; set; }
		public bool LoggedIn { get; set; }
		public bool UserSavedArticle { get; set; }

		public bool UserWroteArticle { get; set; }
	}
}
