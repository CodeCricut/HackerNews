using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.ViewComponents
{
	public class HomeSidebarCardViewComponent : ViewComponent
	{
		private readonly IApiReader _apiReader;

		public HomeSidebarCardViewComponent(IApiReader apiReader)
		{
			_apiReader = apiReader;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await _apiReader.GetEndpointAsync<GetPrivateUserModel>("users/me");

			var loggedIn = user != null && user.Id > 0;
			var model = new HomeSidebarCardViewModel { LoggedIn = loggedIn };
			return View(model);
		}
	}
}
