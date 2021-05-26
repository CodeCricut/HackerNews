using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration
{
	public class WritePublicUserPageCommand : WriteEntityPageCommand<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public WritePublicUserPageCommand(PaginatedList<GetPublicUserModel> entityPage, IFileOptions fileOptions, PublicUserInclusionConfiguration inclusionConfig) : base(entityPage, fileOptions, inclusionConfig)
		{
		}
	}

	public class WriteUserPageCommandHandler : WriteEntityPageCommandHandler<WritePublicUserPageCommand, GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public WriteUserPageCommandHandler(IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration> configEntityWriter) : base(configEntityWriter)
		{
		}
	}
}
