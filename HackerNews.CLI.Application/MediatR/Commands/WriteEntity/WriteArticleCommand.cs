using HackerNews.CLI.FileWriters;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntity
{
	public class WriteArticleCommand : WriteEntityCommand<GetArticleModel>
	{
		public WriteArticleCommand(GetArticleModel entity, IFileOptions options) : base(entity, options)
		{
		}
	}

	public class WriteArticleCommandHandler : WriteEntityCommandHandler<WriteArticleCommand, GetArticleModel>
	{
		public WriteArticleCommandHandler(IEntityWriter<GetArticleModel> entityWriter) : base(entityWriter)
		{
		}
	}
}
