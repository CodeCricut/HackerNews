using HackerNews.CLI.Loggers;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.LogEntity
{
	public class LogArticleCommand : LogEntityCommand<GetArticleModel>
	{
		public LogArticleCommand(GetArticleModel entity, IPrintOptions options) : base(entity, options)
		{
		}
	}

	public class LogArticleCommandHandler : LogEntityCommandHandler<LogArticleCommand, GetArticleModel>
	{
		public LogArticleCommandHandler(IEntityLogger<GetArticleModel> entityLogger) : base(entityLogger)
		{
		}
	}
}
