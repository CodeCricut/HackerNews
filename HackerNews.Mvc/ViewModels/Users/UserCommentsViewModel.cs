using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserCommentsViewModel
	{
		public FrontendPage<GetCommentModel> CommentPage { get; set; }
	}
}
