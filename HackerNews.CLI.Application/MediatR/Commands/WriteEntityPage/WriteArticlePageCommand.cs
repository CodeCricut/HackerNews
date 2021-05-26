using HackerNews.CLI.FileWriters;
using HackerNews.CLI.MediatR.Commands.WriteEntityPage;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityPage
{
	public class WriteArticlePageCommand : WriteEntityPageCommand<GetArticleModel>
	{
		public WriteArticlePageCommand(PaginatedList<GetArticleModel> entityPage, IFileOptions fileOptions) : base(entityPage, fileOptions)
		{
		}
	}

	public class WriteArticlePageCommandHandler : WriteEntityPageCommandHandler<WriteArticlePageCommand, GetArticleModel>
	{
		public WriteArticlePageCommandHandler(IEntityWriter<GetArticleModel> entityWriter) : base(entityWriter)
		{
		}
	}
}
