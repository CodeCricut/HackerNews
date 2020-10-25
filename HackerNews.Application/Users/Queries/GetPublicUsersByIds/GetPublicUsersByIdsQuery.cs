using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetPublicUsersByIds
{
	public class GetPublicUsersByIdsQuery : IRequest<PaginatedList<GetPublicUserModel>>
	{
		public GetPublicUsersByIdsQuery(IEnumerable<int> ids, PagingParams pagingParams)
		{
			Ids = ids;
			PagingParams = pagingParams;
		}

		public IEnumerable<int> Ids { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetPublicUsersByIdsHandler : DatabaseRequestHandler<GetPublicUsersByIdsQuery, PaginatedList<GetPublicUserModel>>
	{
		public GetPublicUsersByIdsHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<PaginatedList<GetPublicUserModel>> Handle(GetPublicUsersByIdsQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var users = await UnitOfWork.Users.GetEntitiesAsync();
				var usersByIds = users.Where(user => request.Ids.Contains(user.Id));
				var paginatedUsers = await usersByIds.PaginatedListAsync(request.PagingParams);

				return Mapper.Map<PaginatedList<GetPublicUserModel>>(paginatedUsers);
			}
		}
	}
}
