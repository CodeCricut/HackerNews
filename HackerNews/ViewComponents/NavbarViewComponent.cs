using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.ViewComponents
{
	public class NavbarViewComponent : ViewComponent
	{
		private readonly IApiReader _apiReader;

		public NavbarViewComponent(IApiReader apiReader)
		{
			_apiReader = apiReader;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var privateUser = await _apiReader.GetEndpointAsync<GetPrivateUserModel>("users/me");

			var model = new NavbarViewModel { User = privateUser };

			return View(model);
		}
	}
}
