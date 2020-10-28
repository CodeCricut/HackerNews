using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardAdminViewModel
	{
		public GetBoardModel Board { get; set; }
		public int ModeratorAddedId { get; set; }
		public bool UserCreatedBoard { get; set; }

		public FrontendPage<GetPublicUserModel> ModeratorPage { get; set; }
	}
}
