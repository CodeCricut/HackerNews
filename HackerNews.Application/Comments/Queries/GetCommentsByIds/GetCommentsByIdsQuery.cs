using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Comments.Queries.GetCommentsByIds
{
	public class GetCommentsByIdsQuery : IRequest<PaginatedList<GetCommentModel>>
	{
		public GetCommentsByIdsQuery(IEnumerable<int> ids, PagingParams pagingParams)
		{
			Ids = ids;
			PagingParams = pagingParams;
		}

		public IEnumerable<int> Ids { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetCommentsByIdsHandler : DatabaseRequestHandler<GetCommentsByIdsQuery, PaginatedList<GetCommentModel>>
	{
		public GetCommentsByIdsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetCommentModel>> Handle(GetCommentsByIdsQuery request, CancellationToken cancellationToken)
		{
			var comments = await UnitOfWork.Comments.GetEntitiesAsync();
			var commentsByIds = comments.Where(comment => request.Ids.Contains(comment.Id));
			var paginatedComments = await commentsByIds.PaginatedListAsync(request.PagingParams);

			return paginatedComments.ToMappedPagedList<Comment, GetCommentModel>(Mapper);
		}
	}
}
