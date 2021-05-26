using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Queries.GetEntitiesByIds
{
	public class GetPublicUsersByIdsQuery : GetEntitiesByIdsQuery<GetPublicUserModel>
	{
		public GetPublicUsersByIdsQuery(IIdListOptions idsOptions, IPageOptions pageOptions) : base(idsOptions, pageOptions)
		{
		}
	}

	public class GetUsersByIdsQueryHandler : GetEntitiesByIdsQueryHandler<GetPublicUsersByIdsQuery, GetPublicUserModel>
	{
		public GetUsersByIdsQueryHandler(IEntityFinder<GetPublicUserModel> entityFinder) : base(entityFinder)
		{
		}
	}
}
