using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public GetCommentsByIdsHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<PaginatedList<GetCommentModel>> Handle(GetCommentsByIdsQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var comments = await UnitOfWork.Comments.GetEntitiesAsync();
				var commentsByIds = comments.Where(comment => request.Ids.Contains(comment.Id));
				var paginatedComments = await commentsByIds.PaginatedListAsync(request.PagingParams);

				return Mapper.Map<PaginatedList<GetCommentModel>>(paginatedComments);
			}
		}
	}
}
