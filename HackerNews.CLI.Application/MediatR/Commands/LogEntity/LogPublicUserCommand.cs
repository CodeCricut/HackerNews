using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntity
{
	public class LogPublicUserCommand : LogEntityCommand<GetPublicUserModel>
	{
		public LogPublicUserCommand(GetPublicUserModel entity, IPrintOptions options) : base(entity, options)
		{
		}
	}

	public class LogUserCommandHandler : LogEntityCommandHandler<LogPublicUserCommand, GetPublicUserModel>
	{
		public LogUserCommandHandler(IEntityLogger<GetPublicUserModel> entityLogger) : base(entityLogger)
		{
		}
	}
}
