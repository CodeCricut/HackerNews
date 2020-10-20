using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
