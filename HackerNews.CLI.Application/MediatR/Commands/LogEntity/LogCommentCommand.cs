using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntity
{
	public class LogCommentCommand : LogEntityCommand<GetCommentModel>
	{
		public LogCommentCommand(GetCommentModel entity, IPrintOptions options) : base(entity, options)
		{
		}
	}

	public class LogCommentCommandHandler : LogEntityCommandHandler<LogCommentCommand, GetCommentModel>
	{
		public LogCommentCommandHandler(IEntityLogger<GetCommentModel> entityLogger) : base(entityLogger)
		{
		}
	}
}
