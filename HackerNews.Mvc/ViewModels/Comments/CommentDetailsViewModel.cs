using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Comments
{
	public class CommentDetailsViewModel
	{
		public GetCommentModel Comment { get; set; }

		public GetPublicUserModel User { get; set; }
		public GetBoardModel Board { get; set; }
		public GetCommentModel ParentComment { get; set; }
		public GetArticleModel ParentArticle { get; set; }

		public FrontendPage<GetCommentModel> ChildCommentPage { get; set; }

		public PostCommentModel PostCommentModel { get; set; }

		public bool LoggedIn { get; set; }
		public bool UserSavedComment { get; set; }
		public bool UserWroteComment { get; set; }
	}
}
