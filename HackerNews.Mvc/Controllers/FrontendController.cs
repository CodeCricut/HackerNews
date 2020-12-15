using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Mvc.Controllers
{
	/// <summary>
	/// A base MVC controller.
	/// </summary>
	public class FrontendController : Controller
	{
		private IMediator _mediator;
		public IMediator Mediator
		{
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
