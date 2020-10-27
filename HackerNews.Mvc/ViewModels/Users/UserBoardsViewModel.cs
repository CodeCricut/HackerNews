using HackerNews.Application.Common.Models.Boards;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserBoardsViewModel
	{
		public FrontendPage<GetBoardModel> BoardsSubscribedPage { get; set; }
		public FrontendPage<GetBoardModel> BoardsModeratingPage { get; set; }
	}
}
