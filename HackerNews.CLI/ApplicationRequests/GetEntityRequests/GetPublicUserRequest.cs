using HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration;
using HackerNews.CLI.Application.MediatR.Queries.GetEntityById;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public class GetPublicUserRequest : IGetEntityRequest<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntityQuery<GetPublicUserModel> CreateGetEntityQuery { get; }

		public CreateLogEntityCommand<GetPublicUserModel, PublicUserInclusionConfiguration> CreateLogEntityCommand { get; }

		public CreateWriteEntityCommand<GetPublicUserModel, PublicUserInclusionConfiguration> CreateWriteEntityCommand { get; }

		public GetPublicUserRequest(GetPublicUserByIdOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntityQuery = () => new GetPublicUserByIdQuery(opts);
			CreateLogEntityCommand = article => new LogPublicUserCommand(article, opts, opts.ToInclusionConfiguration());
			CreateWriteEntityCommand = article => new WritePublicUserCommand(article, opts, opts.ToInclusionConfiguration());
		}
	}
}
