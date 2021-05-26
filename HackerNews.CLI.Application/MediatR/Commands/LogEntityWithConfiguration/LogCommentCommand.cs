using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration
{
	public class LogCommentCommand : LogEntityCommand<GetCommentModel, CommentInclusionConfiguration>
	{
		public LogCommentCommand(GetCommentModel entity, IPrintOptions options, CommentInclusionConfiguration inclusionConfig) : base(entity, options, inclusionConfig)
		{
		}
	}

	public class LogCommentCommandHandler : LogEntityCommandHandler<LogCommentCommand, GetCommentModel, CommentInclusionConfiguration>
	{
		public LogCommentCommandHandler(IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
