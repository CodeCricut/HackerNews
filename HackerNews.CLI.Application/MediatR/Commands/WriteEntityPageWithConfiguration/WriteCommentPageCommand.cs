using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration
{
	public class WriteCommentPageCommand : WriteEntityPageCommand<GetCommentModel, CommentInclusionConfiguration>
	{
		public WriteCommentPageCommand(PaginatedList<GetCommentModel> entityPage, IFileOptions fileOptions, CommentInclusionConfiguration inclusionConfig) : base(entityPage, fileOptions, inclusionConfig)
		{
		}
	}

	public class WriteCommentPageCommandHandler : WriteEntityPageCommandHandler<WriteCommentPageCommand, GetCommentModel, CommentInclusionConfiguration>
	{
		public WriteCommentPageCommandHandler(IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> configEntityWriter) : base(configEntityWriter)
		{
		}
	}
}
