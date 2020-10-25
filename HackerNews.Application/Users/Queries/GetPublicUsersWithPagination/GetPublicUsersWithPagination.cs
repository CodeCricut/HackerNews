using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetPublicUsersWithPagination
{
	public class GetPublicUsersWithPagination : IRequest<PaginatedList<GetPublicUserModel>>
	{
		public GetPublicUsersWithPagination(PagingParams pagingParams)
		{
			PagingParams = pagingParams;
		}

		public PagingParams PagingParams { get; }
	}

	public class GetPublicUsersWithPaginationHandler : DatabaseRequestHandler<GetPublicUsersWithPagination, PaginatedList<GetPublicUserModel>>
	{
		public GetPublicUsersWithPaginationHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<PaginatedList<GetPublicUserModel>> Handle(GetPublicUsersWithPagination request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var users = await UnitOfWork.Users.GetEntitiesAsync();
				var paginatedUsers = await users.PaginatedListAsync(request.PagingParams);

				return Mapper.Map<PaginatedList<GetPublicUserModel>>(paginatedUsers);
			}
		}
	}
}
