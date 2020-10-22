using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Boards
{
	public class BoardModeratorsListViewModel
	{
		public GetBoardModel Board { get; set; }
		public IEnumerable<GetPublicUserModel> Moderators { get; set; }
	}
}
