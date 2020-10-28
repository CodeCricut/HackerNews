using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardDetailsViewModel
	{
		public GetBoardModel Board { get; set; }
		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
		public FrontendPage<GetPublicUserModel> Moderators { get; set; }
	}
}
