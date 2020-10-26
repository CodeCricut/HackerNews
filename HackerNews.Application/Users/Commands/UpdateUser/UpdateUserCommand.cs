using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Commands.UpdateUser
{
	public class UpdateUserCommand : IRequest<GetPrivateUserModel>
	{
		public UpdateUserCommand(UpdateUserModel updateUserModel)
		{
			UpdateUserModel = updateUserModel;
		}

		public UpdateUserModel UpdateUserModel { get; }
	}

	public class UpdateUserHandler : DatabaseRequestHandler<UpdateUserCommand, GetPrivateUserModel>
	{
		private readonly ICurrentUserService _currentUserService;

		public UpdateUserHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<GetPrivateUserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var userid = _currentUserService.UserId;
				if (!await UnitOfWork.Users.EntityExistsAsync(userid)) throw new UnauthorizedException();
				var currentUser = await UnitOfWork.Users.GetEntityAsync(userid);

				// Update user.
				var updateModel = request.UpdateUserModel;
				currentUser.FirstName = updateModel.FirstName;
				currentUser.LastName = updateModel.LastName;
				currentUser.Password = updateModel.Password;

				// Update and save.
				await UnitOfWork.Users.UpdateEntityAsync(userid, currentUser);
				UnitOfWork.SaveChanges();

				return Mapper.Map<GetPrivateUserModel>(currentUser);
			}
		}
	}
}
