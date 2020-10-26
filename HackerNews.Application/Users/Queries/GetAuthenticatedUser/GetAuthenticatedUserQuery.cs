using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetAuthenticatedUser
{
	public class GetAuthenticatedUserQuery : IRequest<GetPrivateUserModel>
	{
	}

	public class GetPrivateUserHandler : DatabaseRequestHandler<GetAuthenticatedUserQuery, GetPrivateUserModel>
	{
		private readonly ICurrentUserService _currentUserService;

		public GetPrivateUserHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<GetPrivateUserModel> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.UserId;

			using (UnitOfWork)
			{
				var user = await UnitOfWork.Users.GetEntityAsync(userId);
				if (user == null) throw new NotFoundException();

				return Mapper.Map<GetPrivateUserModel>(user);
			}
		}
	}
}
