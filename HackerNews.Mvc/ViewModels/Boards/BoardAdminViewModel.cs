using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardAdminViewModel
	{
		public GetBoardModel Board { get; set; }
		public string ModeratorAddedUsername { get; set; }
		public bool UserCreatedBoard { get; set; }

		public FrontendPage<GetPublicUserModel> ModeratorPage { get; set; }
	}
}
