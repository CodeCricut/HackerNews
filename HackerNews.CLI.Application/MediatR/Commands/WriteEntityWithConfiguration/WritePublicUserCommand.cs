using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration
{
	public class WritePublicUserCommand : WriteEntityCommand<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public WritePublicUserCommand(GetPublicUserModel entity, IFileOptions options, PublicUserInclusionConfiguration inclusionConfig) : base(entity, options, inclusionConfig)
		{
		}
	}

	public class WriteUserCommandHandler : WriteEntityCommandHandler<WritePublicUserCommand, GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public WriteUserCommandHandler(IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration> configEntityWriter) : base(configEntityWriter)
		{
		}
	}
}
