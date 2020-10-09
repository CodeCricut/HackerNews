using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Boards
{
	public class BoardAdminViewModel
	{
		public GetBoardModel Board { get; set; }
		public int ModeratorAddedId { get; set; }
		public IEnumerable<GetPublicUserModel> Moderators { get; set; }
		public IEnumerable<GetPublicUserModel> Subscribers { get; set; }
	}
}
