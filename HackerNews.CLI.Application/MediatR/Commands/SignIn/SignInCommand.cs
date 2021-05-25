using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Options;
using HackerNews.Domain.Common.Models.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.SignIn
{
	public class SignInCommand : IRequest<bool>
	{
		public SignInCommand(ILoginOptions loginOptions)
		{
			LoginOptions = loginOptions;
		}

		public ILoginOptions LoginOptions { get; }
	}

	public class SignInCommandHandler : IRequestHandler<SignInCommand, bool>
	{
		private readonly ISignInManager _signInManager;
		private readonly ILogger<SignInCommandHandler> _logger;

		public SignInCommandHandler(ISignInManager signInManager,
			ILogger<SignInCommandHandler> logger)
		{
			_signInManager = signInManager;
			_logger = logger;
		}


		public async Task<bool> Handle(SignInCommand request, CancellationToken cancellationToken)
		{
			try
			{
				// TODO: create converter or extension method
				LoginModel loginModel = new LoginModel()
				{
					UserName = request.LoginOptions.Username,
					Password = request.LoginOptions.Password
				};
				await _signInManager.SignInAsync(loginModel);

				return true;
			}
			catch (Exception e)
			{
				_logger.LogWarning("Could not sign in successfully.");
				return false;
			}
		}
	}
}
