using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Home
{
	public class HomeSearchViewModel
	{
		public string SearchTerm { get; set; }
		public FrontendPage<GetBoardModel> BoardPage { get; set; }
		public FrontendPage<GetPublicUserModel> UserPage { get; set; }
		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
		public FrontendPage<GetCommentModel> CommentPage { get; set; }
	}
}
