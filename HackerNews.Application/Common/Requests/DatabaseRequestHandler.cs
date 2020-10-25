using AutoMapper;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Requests
{
	public abstract class DatabaseRequestHandler<TRequest, TReturn> : IRequestHandler<TRequest, TReturn>
		where TRequest : IRequest<TReturn>
	{
		private HttpContext _httpContext;

		public DatabaseRequestHandler(IHttpContextAccessor httpContextAccessor)
		{
			_httpContext = httpContextAccessor.HttpContext;
		}

		protected IUnitOfWork UnitOfWork { get => _httpContext.RequestServices.GetService<IUnitOfWork>(); }
		protected IMediator Mediator { get => _httpContext.RequestServices.GetService<IMediator>(); }
		protected IMapper Mapper { get => _httpContext.RequestServices.GetService<IMapper>(); }

		public abstract Task<TReturn> Handle(TRequest request, CancellationToken cancellationToken);
	}
}
