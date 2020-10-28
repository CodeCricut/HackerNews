using HackerNews.Application.Common.Models.Boards;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserBoardsViewModel
	{
		public FrontendPage<GetBoardModel> BoardsSubscribedPage { get; set; }
		public FrontendPage<GetBoardModel> BoardsModeratingPage { get; set; }
	}
}
