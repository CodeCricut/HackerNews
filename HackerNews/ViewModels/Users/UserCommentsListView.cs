using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;

namespace HackerNews.ViewModels.Users
{
	public class UserCommentsListView
	{
		public Page<GetCommentModel> CommentPage { get; set; }
	}
}
