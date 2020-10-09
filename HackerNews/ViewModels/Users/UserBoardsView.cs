using HackerNews.Domain.Models.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Users
{
	public class UserBoardsView
	{
		public IEnumerable<GetBoardModel> BoardsSubscribed { get; set; }
		public IEnumerable<GetBoardModel> BoardsModerating { get; set; }
	}
}
