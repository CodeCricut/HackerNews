using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardModeratorsViewModel
	{
		public GetBoardModel Board { get; set; }
		public FrontendPage<GetPublicUserModel> ModeratorPage { get; set; }
	}
}
