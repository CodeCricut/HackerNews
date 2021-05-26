using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.LogEntities;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntityPage
{
	public class LogArticlePageCommand : LogEntityPageCommand<GetArticleModel>
	{
		public LogArticlePageCommand(PaginatedList<GetArticleModel> entities, IPrintOptions printOptions) : base(entities, printOptions)
		{
		}
	}

	public class LogArticlePageCommandHandler : LogEntityPageCommandHandler<LogArticlePageCommand, GetArticleModel>
	{
		public LogArticlePageCommandHandler(IEntityLogger<GetArticleModel> entityLogger) : base(entityLogger)
		{
		}
	}
}
