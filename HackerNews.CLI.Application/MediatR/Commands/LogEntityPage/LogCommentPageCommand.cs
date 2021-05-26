using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.LogEntities;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPage
{
	public class LogCommentPageCommand : LogEntityPageCommand<GetCommentModel>
	{
		public LogCommentPageCommand(PaginatedList<GetCommentModel> entities, IPrintOptions printOptions) : base(entities, printOptions)
		{
		}
	}

	public class LogCommentPageCommandHandler : LogEntityPageCommandHandler<LogCommentPageCommand, GetCommentModel>
	{
		public LogCommentPageCommandHandler(IEntityLogger<GetCommentModel> entityLogger) : base(entityLogger)
		{
		}
	}
}
