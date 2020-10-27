using HackerNews.Application.Common.Models.Boards;
using HackerNews.Mvc.ViewModels.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewComponents
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
