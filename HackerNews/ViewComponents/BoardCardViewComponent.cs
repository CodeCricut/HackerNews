using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewComponents
{
	public class BoardCardViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(GetBoardModel boardModel)
		{
			var model = new BoardCardViewModel { Board = boardModel };
			return View(model);
		}
	}
}
