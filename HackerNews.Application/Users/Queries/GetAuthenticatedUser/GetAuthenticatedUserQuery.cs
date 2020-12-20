using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetAuthenticatedUser
{
	public class GetAuthenticatedUserQuery : IRequest<GetPrivateUserModel>
	{
	}

	public class GetAuthenticatedUserHandler : DatabaseRequestHandler<GetAuthenticatedUserQuery, GetPrivateUserModel>
	{
		private readonly UserManager<User> _userManager;

		public GetAuthenticatedUserHandler(UserManager<User> userManager,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_userManager = userManager;
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
