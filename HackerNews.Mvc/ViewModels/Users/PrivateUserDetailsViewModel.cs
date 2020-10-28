using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class PrivateUserDetailsViewModel
	{
		public GetPrivateUserModel User { get; set; }

		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
		public FrontendPage<GetCommentModel> CommentPage { get; set; }
	}
}
