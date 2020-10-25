using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
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
				var registeredUser = await UnitOfWork.Users.AddEntityAsync(user);

				return Mapper.Map<GetPrivateUserModel>(registeredUser);
			}
		}
	}
}
