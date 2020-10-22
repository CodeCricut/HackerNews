using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Boards
{
	public class BoardAdminViewModel
	{
		public GetBoardModel Board { get; set; }
		public int ModeratorAddedId { get; set; }

		public Page<GetPublicUserModel> ModeratorPage { get; set; }
	}
}
