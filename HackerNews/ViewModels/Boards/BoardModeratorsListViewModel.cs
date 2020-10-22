using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Boards
{
	public class BoardModeratorsListViewModel
	{
		public GetBoardModel Board { get; set; }
		public Page<GetPublicUserModel> ModeratorPage { get; set; }
	}
}
