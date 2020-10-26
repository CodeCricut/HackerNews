using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
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
		public RegisterUserHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetPrivateUserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
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
}
