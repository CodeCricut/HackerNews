using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.ViewModels.Base;
using System.Collections;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Comments
{
	public class CommentDetailsViewModel : DetailsViewModel<GetCommentModel>
	{
		public GetPublicUserModel User { get; set; }
		public GetBoardModel Board { get; set; }
		public GetCommentModel ParentComment { get; set; }
		public GetArticleModel ParentArticle { get; set; }
		public IEnumerable<GetCommentModel> ChildComments { get; set; }
		public PostCommentModel PostCommentModel { get; set; }

		public bool LoggedIn { get; set; }
		public bool UserSavedComment { get; set; }
	}
}
