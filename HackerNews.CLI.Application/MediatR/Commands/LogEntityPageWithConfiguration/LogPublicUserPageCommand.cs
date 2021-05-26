using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration
{
	public class LogPublicUserPageCommand : LogEntityPageCommand<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public LogPublicUserPageCommand(PaginatedList<GetPublicUserModel> entityPage, IPrintOptions printOptions, PublicUserInclusionConfiguration inclusionConfig) : base(entityPage, printOptions, inclusionConfig)
		{
		}
	}

	public class LogUserPageCommandHandler : LogEntityPageCommandHandler<LogPublicUserPageCommand, GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public LogUserPageCommandHandler(IConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
