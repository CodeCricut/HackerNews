using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration
{
	public class WriteArticlePageCommand : WriteEntityPageCommand<GetArticleModel, ArticleInclusionConfiguration>
	{
		public WriteArticlePageCommand(PaginatedList<GetArticleModel> entityPage, IFileOptions fileOptions, ArticleInclusionConfiguration inclusionConfig) : base(entityPage, fileOptions, inclusionConfig)
		{
		}
	}

	public class WriteArticlePageCommandHandler : WriteEntityPageCommandHandler<WriteArticlePageCommand, GetArticleModel, ArticleInclusionConfiguration>
	{
		public WriteArticlePageCommandHandler(IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> configEntityWriter) : base(configEntityWriter)
		{
		}
	}
}
