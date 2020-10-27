using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Mappings;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetPublicUsersWithPagination
{
	public class GetPublicUsersWithPaginationQuery : IRequest<PaginatedList<GetPublicUserModel>>
	{
		public GetPublicUsersWithPaginationQuery(PagingParams pagingParams)
		{
			PagingParams = pagingParams;
		}

		public PagingParams PagingParams { get; }
	}

	public class GetPublicUsersWithPaginationHandler : DatabaseRequestHandler<GetPublicUsersWithPaginationQuery, PaginatedList<GetPublicUserModel>>
	{
		public GetPublicUsersWithPaginationHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<GetPublicUserModel>> Handle(GetPublicUsersWithPaginationQuery request, CancellationToken cancellationToken)
		{
				var users = await UnitOfWork.Users.GetEntitiesAsync();
				var paginatedUsers = await users.PaginatedListAsync(request.PagingParams);

				return paginatedUsers.ToMappedPagedList<User, GetPublicUserModel>(Mapper); 
		}
	}
}
