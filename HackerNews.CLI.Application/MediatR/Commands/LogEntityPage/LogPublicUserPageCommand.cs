using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.LogEntities;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPage
{
	public class LogPublicUserPageCommand : LogEntityPageCommand<GetPublicUserModel>
	{
		public LogPublicUserPageCommand(PaginatedList<GetPublicUserModel> entities, IPrintOptions printOptions) : base(entities, printOptions)
		{
		}
	}

	public class LogUserPageCommandHandler : LogEntityPageCommandHandler<LogPublicUserPageCommand, GetPublicUserModel>
	{
		public LogUserPageCommandHandler(IEntityLogger<GetPublicUserModel> entityLogger) : base(entityLogger)
		{
		}
	}
}
