﻿using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
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
		private readonly IDeletedEntityPolicyValidator<User> _deletedEntityValidator;

		public GetPublicUsersByIdsHandler(IDeletedEntityPolicyValidator<User> deletedEntityValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedEntityValidator = deletedEntityValidator;
		}

		public override async Task<PaginatedList<GetPublicUserModel>> Handle(GetPublicUsersByIdsQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var usersByIds = users.Where(user => request.Ids.Contains(user.Id));

			usersByIds = _deletedEntityValidator.ValidateEntityQuerable(usersByIds, Domain.Common.DeletedEntityPolicy.OWNER);

			var paginatedUsers = await usersByIds.PaginatedListAsync(request.PagingParams);

			return paginatedUsers.ToMappedPagedList<User, GetPublicUserModel>(Mapper);
		}
	}
}
