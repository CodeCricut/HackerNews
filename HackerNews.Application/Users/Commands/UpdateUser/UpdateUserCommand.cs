using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
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
		public UpdateUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetPrivateUserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
