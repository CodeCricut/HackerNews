using HackerNews.Application.Common.Models.Boards;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardSearchViewModel
	{
		public string SearchTerm { get; set; }
		public FrontendPage<GetBoardModel> BoardPage { get; set; }
	}
}
