using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Queries.GetEntitiesByIds
{
	public class GetCommentsByIdsQuery : GetEntitiesByIdsQuery<GetCommentModel>
	{
		public GetCommentsByIdsQuery(IIdListOptions idsOptions, IPageOptions pageOptions) : base(idsOptions, pageOptions)
		{
		}
	}

	public class GetCommentsByIdsQueryHandler : GetEntitiesByIdsQueryHandler<GetCommentsByIdsQuery, GetCommentModel>
	{
		public GetCommentsByIdsQueryHandler(IEntityFinder<GetCommentModel> entityFinder) : base(entityFinder)
		{
		}
	}
}
