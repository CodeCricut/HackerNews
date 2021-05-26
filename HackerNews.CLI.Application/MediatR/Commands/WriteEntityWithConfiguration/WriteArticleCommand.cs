using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration
{
	public class WriteArticleCommand : WriteEntityCommand<GetArticleModel, ArticleInclusionConfiguration>
	{
		public WriteArticleCommand(GetArticleModel entity, IFileOptions options, ArticleInclusionConfiguration inclusionConfig) : base(entity, options, inclusionConfig)
		{
		}
	}

	public class WriteArticleCommandHandler : WriteEntityCommandHandler<WriteArticleCommand, GetArticleModel, ArticleInclusionConfiguration>
	{
		public WriteArticleCommandHandler(IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> configEntityWriter) : base(configEntityWriter)
		{
		}
	}
}
