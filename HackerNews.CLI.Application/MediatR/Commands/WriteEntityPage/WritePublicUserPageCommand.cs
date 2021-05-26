using HackerNews.CLI.FileWriters;
using HackerNews.CLI.MediatR.Commands.WriteEntityPage;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityPage
{
	public class WritePublicUserPageCommand : WriteEntityPageCommand<GetPublicUserModel>
	{
		public WritePublicUserPageCommand(PaginatedList<GetPublicUserModel> entityPage, IFileOptions fileOptions) : base(entityPage, fileOptions)
		{
		}
	}

	public class WriteUserPageCommandHandler : WriteEntityPageCommandHandler<WritePublicUserPageCommand, GetPublicUserModel>
	{
		public WriteUserPageCommandHandler(IEntityWriter<GetPublicUserModel> entityWriter) : base(entityWriter)
		{
		}
	}
}
