using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration
{
	public class LogCommentPageCommand : LogEntityPageCommand<GetCommentModel, CommentInclusionConfiguration>
	{
		public LogCommentPageCommand(PaginatedList<GetCommentModel> entityPage, IPrintOptions printOptions, CommentInclusionConfiguration inclusionConfig) : base(entityPage, printOptions, inclusionConfig)
		{
		}
	}

	public class LogCommentPageCommandHandler : LogEntityPageCommandHandler<LogCommentPageCommand, GetCommentModel, CommentInclusionConfiguration>
	{
		public LogCommentPageCommandHandler(IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration> configEntityLogger) : base(configEntityLogger)
		{
		}
	}
}
