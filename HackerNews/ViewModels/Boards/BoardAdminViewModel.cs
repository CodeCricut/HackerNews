using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Boards
{
	public class BoardAdminViewModel
	{
		public GetBoardModel Board { get; set; }
		public int ModeratorAddedId { get; set; }
		public bool UserCreatedBoard { get; set; }

		public Page<GetPublicUserModel> ModeratorPage { get; set; }
	}
}
