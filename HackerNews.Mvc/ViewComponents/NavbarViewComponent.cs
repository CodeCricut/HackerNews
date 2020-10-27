using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.ViewModels.ViewComponents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
			NavbarViewModel model;
			try
			{
				var privateUser = await _mediator.Send(new GetAuthenticatedUserQuery());
				model = new NavbarViewModel { User = privateUser };
			}
			catch (NotFoundException ex)
			{
				model = new NavbarViewModel();
			}

			return View(model);
		}
	}
}
