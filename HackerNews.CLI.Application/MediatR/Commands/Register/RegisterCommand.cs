using HackerNews.ApiConsumer.Account;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.Register
{
	public class RegisterCommand : IRequest<Jwt>
	{
		public RegisterCommand(RegisterUserModel registerUserModel)
		{
			RegisterUserModel = registerUserModel;
		}

		public RegisterUserModel RegisterUserModel { get; }
	}

	public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Jwt>
	{
		private readonly IRegistrationApiClient _registrationApiClient;
		private readonly IJwtLogger _jwtLogger;

		public RegisterCommandHandler(IRegistrationApiClient registrationApiClient,
			IJwtLogger jwtLogger)
		{
			_registrationApiClient = registrationApiClient;
			_jwtLogger = jwtLogger;
		}

		public  Task<Jwt> Handle(RegisterCommand request, CancellationToken cancellationToken)
		{
			return  _registrationApiClient.RegisterAsync(request.RegisterUserModel);
		}
	}
}
