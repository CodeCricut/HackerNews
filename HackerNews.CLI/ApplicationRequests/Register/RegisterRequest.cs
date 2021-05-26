using HackerNews.CLI.Application.MediatR.Commands.LogJwt;
using HackerNews.CLI.Application.MediatR.Commands.Register;
using HackerNews.CLI.Domain.Options;
using HackerNews.CLI.Domain.Util;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.Register
{
	public interface IRegisterRequest
	{
		CreateVerbosityCommand CreateVerbosityCommand { get; }
		CreateRegisterCommand CreateRegisterCommand { get; }
		CreateLogJwtCommand CreateLogJwtCommand { get; }
	}

	public class RegisterRequest : IRegisterRequest
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateRegisterCommand CreateRegisterCommand { get; }

		public CreateLogJwtCommand CreateLogJwtCommand { get; }

		public RegisterRequest(RegisterOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateRegisterCommand = () => new RegisterCommand(opts.ToRegisterUserModel());
			CreateLogJwtCommand = jwt => new LogJwtCommand(jwt);
		}
	}
}
