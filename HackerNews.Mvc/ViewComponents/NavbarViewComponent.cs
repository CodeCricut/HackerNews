using HackerNews.Application.Common.Helpers;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Mvc.ViewModels.ViewComponents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewComponents
{
	public class NavbarViewComponent : ViewComponent
	{
		private readonly IMediator _mediator;

		public NavbarViewComponent(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var privateUser = await new GetAuthenticatedUserQuery().DefaultIfExceptionAsync(_mediator);

			NavbarViewModel model = new NavbarViewModel
			{
				User = privateUser
			};

			return View(model);
		}
	}
}
