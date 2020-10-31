using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.Mvc.ViewModels.ViewComponents.ArticleCard
{
	public class ArticleCardViewModel
	{
		public GetArticleModel Article { get; set; }
		public GetBoardModel Board { get; set; }
		public GetPublicUserModel User { get; set; }
	}
}
