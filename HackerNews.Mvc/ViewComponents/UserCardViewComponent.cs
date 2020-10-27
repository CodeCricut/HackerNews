using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.ViewModels.ViewComponents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
