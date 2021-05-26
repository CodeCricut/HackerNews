using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.MediatR;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Queries.GetEntityById
{
	public class GetCommentByIdQuery : GetEntityByIdQuery<GetCommentModel>
	{
		public GetCommentByIdQuery(IIdOptions options) : base(options)
		{
		}
	}

	public class GetCommentByIdQueryHandler : GetEntityByIdQueryHandler<GetCommentByIdQuery, GetCommentModel>
	{
		public GetCommentByIdQueryHandler(ILogger<GetEntityByIdQueryHandler<GetCommentByIdQuery, GetCommentModel>> logger, IEntityFinder<GetCommentModel> entityFinder) : base(logger, entityFinder)
		{
		}
	}
}
