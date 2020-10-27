using HackerNews.Application.Common.Models.Boards;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Home
{
	public class HomeBoardsViewModel
	{
		public FrontendPage<GetBoardModel> BoardPage { get; set; }
	}
}
