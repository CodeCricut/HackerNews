using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Home
{
	public class HomeSearchViewModel
	{
		public string SearchTerm { get; set; }
		public Page<GetBoardModel> BoardPage { get; set; }
		public Page<GetPublicUserModel> UserPage { get; set; }
		public Page<GetArticleModel> ArticlePage { get; set; }
		public Page<GetCommentModel> CommentPage { get; set; }
	}
}
