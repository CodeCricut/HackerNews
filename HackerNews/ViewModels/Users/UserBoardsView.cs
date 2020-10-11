using HackerNews.Domain.Models.Board;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Users
{
	public class UserBoardsView
	{
		public IEnumerable<GetBoardModel> BoardsSubscribed { get; set; }
		public IEnumerable<GetBoardModel> BoardsModerating { get; set; }
	}
}
