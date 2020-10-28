using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserSavedViewModel
	{
		public FrontendPage<GetArticleModel> SavedArticlesPage { get; set; }
		public FrontendPage<GetCommentModel> SavedCommentsPage { get; set; }
	}
}
