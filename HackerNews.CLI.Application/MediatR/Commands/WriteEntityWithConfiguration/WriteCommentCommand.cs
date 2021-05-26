using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration
{
	public class WriteCommentCommand : WriteEntityCommand<GetCommentModel, CommentInclusionConfiguration>
	{
		public WriteCommentCommand(GetCommentModel entity, IFileOptions options, CommentInclusionConfiguration inclusionConfig) : base(entity, options, inclusionConfig)
		{
		}
	}

	public class WriteCommentCommandHandler : WriteEntityCommandHandler<WriteCommentCommand, GetCommentModel, CommentInclusionConfiguration>
	{
		public WriteCommentCommandHandler(IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> configEntityWriter) : base(configEntityWriter)
		{
		}
	}
}
