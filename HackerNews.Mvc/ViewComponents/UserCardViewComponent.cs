using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.ViewModels.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Mvc.ViewComponents
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
