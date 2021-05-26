using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration
{
	public class LogPublicUserCommand : LogEntityCommand<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public LogPublicUserCommand(GetPublicUserModel entity, IPrintOptions options, PublicUserInclusionConfiguration inclusionConfig) : base(entity, options, inclusionConfig)
		{
		}
	}

	public class LogUserCommandHandler : LogEntityCommandHandler<LogPublicUserCommand, GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public LogUserCommandHandler(IConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
