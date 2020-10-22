using HackerNews.Domain.Models.Board;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;

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
