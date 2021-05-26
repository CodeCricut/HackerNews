using HackerNews.CLI.FileWriters;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntity
{
	public class WritePublicUserCommand : WriteEntityCommand<GetPublicUserModel>
	{
		public WritePublicUserCommand(GetPublicUserModel entity, IFileOptions options) : base(entity, options)
		{
		}
	}

	public class WriteUserCommandHandler : WriteEntityCommandHandler<WritePublicUserCommand, GetPublicUserModel>
	{
		public WriteUserCommandHandler(IEntityWriter<GetPublicUserModel> entityWriter) : base(entityWriter)
		{
		}
	}
}
