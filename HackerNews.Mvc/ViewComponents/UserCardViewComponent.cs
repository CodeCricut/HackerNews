using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.ViewModels.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Mvc.ViewComponents
{
	public class UserCardViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(GetPublicUserModel userModel, string imageDataUrl)
		{
			var model = new UserCardViewModel
			{
				User = userModel,
				ImageDataUrl = imageDataUrl
			};
			return View(model);
		}
	}
}
