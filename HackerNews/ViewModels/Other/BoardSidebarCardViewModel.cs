using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Other
{
	public class BoardSidebarCardViewModel
	{
		public GetBoardModel Board { get; set; }
		public Page<GetPublicUserModel> ModeratorPage { get; set; }
		public bool LoggedIn { get; set; }
		public bool Subscribed { get; set; }
		public bool Moderating { get; set; }
	}
}
