using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Requests
{
	/// <summary>
	/// A base <see cref="IRequestHandler{TRequest, TResponse}"/> providing some common properties used in most
	/// handlers that access the database.
	/// </summary>
	/// <typeparam name="TRequest"></typeparam>
	/// <typeparam name="TReturn"></typeparam>
	public abstract class DatabaseRequestHandler<TRequest, TReturn> : IRequestHandler<TRequest, TReturn>
		where TRequest : IRequest<TReturn>
	{
		public IUnitOfWork UnitOfWork { get; set; }
		public IMediator Mediator { get; set; }
		public IMapper Mapper { get; set; }
		public ICurrentUserService _currentUserService { get; set; }

		public DatabaseRequestHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService)
		{
			UnitOfWork = unitOfWork;
			Mediator = mediator;
			Mapper = mapper;
			_currentUserService = currentUserService;
		}

		public abstract Task<TReturn> Handle(TRequest request, CancellationToken cancellationToken);
	}
}
