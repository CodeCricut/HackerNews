using HackerNews.Domain;
using HackerNews.Domain.Models.Board;

namespace HackerNews.ViewModels.Users
{
	public class UserBoardsView
	{
		public Page<GetBoardModel> BoardsSubscribedPage { get; set; }
		public Page<GetBoardModel> BoardsModeratingPage { get; set; }
	}
}
