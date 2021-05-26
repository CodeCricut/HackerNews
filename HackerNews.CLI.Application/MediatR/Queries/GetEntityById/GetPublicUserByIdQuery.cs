using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.MediatR;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Queries.GetEntityById
{
	public class GetPublicUserByIdQuery : GetEntityByIdQuery<GetPublicUserModel>
	{
		public GetPublicUserByIdQuery(IIdOptions options) : base(options)
		{
		}
	}

	public class GetUserByIdQueryHandler : GetEntityByIdQueryHandler<GetPublicUserByIdQuery, GetPublicUserModel>
	{
		public GetUserByIdQueryHandler(ILogger<GetEntityByIdQueryHandler<GetPublicUserByIdQuery, GetPublicUserModel>> logger, IEntityFinder<GetPublicUserModel> entityFinder) : base(logger, entityFinder)
		{
		}
	}
}
