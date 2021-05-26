using HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration;
using HackerNews.CLI.Application.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public class GetPublicUsersRequest : IGetEntitiesRequest<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntitiesByIdsQuery<GetPublicUserModel> CreateGetEntitiesByIdsQuery { get; }

		public CreateLogEntityPageCommand<GetPublicUserModel, PublicUserInclusionConfiguration> CreateLogEntityPageCommand { get; }

		public CreateWriteEntityPageCommand<GetPublicUserModel, PublicUserInclusionConfiguration> CreateWriteEntityPageCommand { get; }

		public GetPublicUsersRequest(GetPublicUsersOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntitiesByIdsQuery = () => new GetPublicUsersByIdsQuery(opts, opts);
			CreateLogEntityPageCommand = articlePage => new LogPublicUserPageCommand(articlePage, opts, opts.ToInclusionConfiguration());
			CreateWriteEntityPageCommand = articlePage => new WritePublicUserPageCommand(articlePage, opts, opts.ToInclusionConfiguration());
		}
	}
}
