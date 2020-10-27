using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.ViewComponents
{
	public class BoardSidebarViewModel
	{
		public GetBoardModel Board { get; set; }
		public FrontendPage<GetPublicUserModel> ModeratorPage { get; set; }
		public bool LoggedIn { get; set; }
		public bool Subscribed { get; set; }
		public bool Moderating { get; set; }
	}
}
