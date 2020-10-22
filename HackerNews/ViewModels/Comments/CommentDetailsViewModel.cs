using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Comments
{
	public class CommentDetailsViewModel 
	{
		public GetCommentModel Comment { get; set; }

		public GetPublicUserModel User { get; set; }
		public GetBoardModel Board { get; set; }
		public GetCommentModel ParentComment { get; set; }
		public GetArticleModel ParentArticle { get; set; }

		public Page<GetCommentModel> ChildCommentPage { get; set; }

		public PostCommentModel PostCommentModel { get; set; }

		public bool LoggedIn { get; set; }
		public bool UserSavedComment { get; set; }
	}
}
