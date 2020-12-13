using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardDetailsViewModel
	{
		public GetBoardModel Board { get; set; }
		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
		public FrontendPage<GetPublicUserModel> Moderators { get; set; }

		public string ImageDataUrl { get; set; }
	}
}
