using HackerNews.CLI.FileWriters;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntity
{
	public class WriteCommentCommand : WriteEntityCommand<GetCommentModel>
	{
		public WriteCommentCommand(GetCommentModel entity, IFileOptions options) : base(entity, options)
		{
		}
	}

	public class WriteCommentCommandHandler : WriteEntityCommandHandler<WriteCommentCommand, GetCommentModel>
	{
		public WriteCommentCommandHandler(IEntityWriter<GetCommentModel> entityWriter) : base(entityWriter)
		{
		}
	}
}
