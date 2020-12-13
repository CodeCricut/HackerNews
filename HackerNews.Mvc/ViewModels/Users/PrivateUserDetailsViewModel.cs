using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class PrivateUserDetailsViewModel
	{
		public GetPrivateUserModel User { get; set; }

		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
		public FrontendPage<GetCommentModel> CommentPage { get; set; }
		public string ImageDataUrl { get; set; }
	}
}
