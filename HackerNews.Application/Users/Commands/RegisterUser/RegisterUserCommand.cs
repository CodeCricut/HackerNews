using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommand : IRequest<GetPrivateUserModel>
	{
		public RegisterUserCommand(RegisterUserModel registerUserModel)
		{
			RegisterUserModel = registerUserModel;
		}

		public RegisterUserModel RegisterUserModel { get; }
	}

	public class RegisterUserHandler : DatabaseRequestHandler<RegisterUserCommand, GetPrivateUserModel>
	{
		public RegisterUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetPrivateUserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			// var use r= new IdentityUser { ... }
			// var result = await userManager.CreateAsync(user, model.password);
			var user = Mapper.Map<User>(request.RegisterUserModel);

			// Verify username isn't taken
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var userWithUsername = users.FirstOrDefault(u => u.Username == user.Username);
			if (userWithUsername != null) throw new UsernameTakenException();


			user.JoinDate = DateTime.Now;

			var registeredUser = await UnitOfWork.Users.AddEntityAsync(user);

			UnitOfWork.SaveChanges();

			return Mapper.Map<GetPrivateUserModel>(registeredUser);
		}
	}
}
