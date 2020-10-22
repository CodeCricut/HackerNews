using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Users
{
	public class PrivateUserDetailsViewModel
	{
		public GetPrivateUserModel User { get; set; }

		public Page<GetArticleModel> ArticlePage { get; set; }
		public Page<GetCommentModel> CommentPage { get; set; }
	}
}
