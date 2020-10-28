using HackerNews.Application.Common.Interfaces;
using HackerNews.Mvc.ViewModels.ViewComponents.HomeSidebarCard;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Mvc.ViewComponents
{
	public class HomeSidebarCardViewComponent : ViewComponent
	{
		private readonly ICurrentUserService _currentUserService;

		public HomeSidebarCardViewComponent(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		public IViewComponentResult Invoke()
		{
			var loggedIn = _currentUserService.UserId > 0;

			var viewModel = new HomeSidebarCardViewModel { LoggedIn = loggedIn };
			return View(viewModel);
		}
	}
}
