using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.ViewComponents
{
	public class BoardSidebarViewModel
	{
		public GetBoardModel Board { get; set; }
		public FrontendPage<GetPublicUserModel> ModeratorPage { get; set; }
		public bool LoggedIn { get; set; }
		public bool Subscribed { get; set; }
		public bool Moderating { get; set; }
	}
}
