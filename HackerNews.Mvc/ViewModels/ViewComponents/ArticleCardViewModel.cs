using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.Mvc.ViewModels.ViewComponents.ArticleCard
{
	public class ArticleCardViewModel
	{
		public GetArticleModel Article { get; set; }
		public string ImageDataUrl { get; set; }
		public GetBoardModel Board { get; set; }
		public GetPublicUserModel User { get; set; }
		public string Jwt { get; set; }

		// TODO: properly bind value and add js action to make save button work
		public bool LoggedIn { get; set; }
		public bool Saved { get; set; }
		public bool UserUpvoted { get; set; }
		public bool UserDownvoted { get; set; }
	}
}
