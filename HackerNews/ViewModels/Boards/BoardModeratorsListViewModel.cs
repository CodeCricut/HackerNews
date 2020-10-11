using HackerNews.Domain.Models.Users;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Boards
{
	public class BoardModeratorsListViewModel
	{
		public IEnumerable<GetPublicUserModel> Moderators { get; set; }
	}
}
