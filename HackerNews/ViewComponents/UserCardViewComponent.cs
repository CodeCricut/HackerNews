using HackerNews.Domain.Models.Users;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.ViewComponents
{
	public class UserCardViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(GetPublicUserModel userModel)
		{
			var model = new UserCardViewModel
			{
				User = userModel,
			};

			return View(model);
		}
	}
}
