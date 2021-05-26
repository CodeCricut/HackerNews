using HackerNews.CLI.FileWriters;
using HackerNews.CLI.MediatR.Commands.WriteEntityPage;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityPage
{
	public class WriteCommentPageCommand : WriteEntityPageCommand<GetCommentModel>
	{
		public WriteCommentPageCommand(PaginatedList<GetCommentModel> entityPage, IFileOptions fileOptions) : base(entityPage, fileOptions)
		{
		}
	}

	public class WriteCommentPageCommandHandler : WriteEntityPageCommandHandler<WriteCommentPageCommand, GetCommentModel>
	{
		public WriteCommentPageCommandHandler(IEntityWriter<GetCommentModel> entityWriter) : base(entityWriter)
		{
		}
	}
}
