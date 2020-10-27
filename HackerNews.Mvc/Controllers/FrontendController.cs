using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Controllers
{
	public class FrontendController : Controller
	{
		private IMediator _mediator;
		public IMediator Mediator {
			get
			{
				if (_mediator == null)
				{
					_mediator = HttpContext.RequestServices.GetService<IMediator>();
				}
				return _mediator;
			}
			set => _mediator = value;
		}
	}
}
