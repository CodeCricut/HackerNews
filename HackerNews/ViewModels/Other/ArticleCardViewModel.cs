using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Other
{
	public class ArticleCardViewModel
	{
		public GetArticleModel Article { get; set; }
		public GetBoardModel Board { get; set; }
		public GetPublicUserModel User { get; set; }
	}
}
