using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Mvc.ViewModels.ViewComponents;
using Microsoft.AspNetCore.Mvc;

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
