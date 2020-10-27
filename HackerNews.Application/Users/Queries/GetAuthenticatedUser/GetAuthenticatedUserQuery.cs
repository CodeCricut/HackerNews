using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetAuthenticatedUser
{
	public class GetAuthenticatedUserQuery : IRequest<GetPrivateUserModel>
	{
	}

	public class GetAuthenticatedUserHandler : DatabaseRequestHandler<GetAuthenticatedUserQuery, GetPrivateUserModel>
	{
		public GetAuthenticatedUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetPrivateUserModel> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.UserId;

			var user = await UnitOfWork.Users.GetEntityAsync(userId);
			if (user == null) throw new NotFoundException();

			return Mapper.Map<GetPrivateUserModel>(user);
		}
	}
}
