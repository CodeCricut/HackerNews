﻿using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
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
		private readonly IDeletedEntityPolicyValidator<Comment> _deletedCommentValidator;

		public GetCommentsByIdsHandler(IDeletedEntityPolicyValidator<Comment> deletedCommentValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedCommentValidator = deletedCommentValidator;
		}

		public override async Task<PaginatedList<GetCommentModel>> Handle(GetCommentsByIdsQuery request, CancellationToken cancellationToken)
		{
			var comments = await UnitOfWork.Comments.GetEntitiesAsync();
			var commentsByIds = comments.Where(comment => request.Ids.Contains(comment.Id));

			commentsByIds = _deletedCommentValidator.ValidateEntityQuerable(commentsByIds, Domain.Common.DeletedEntityPolicy.OWNER);

			var paginatedComments = await commentsByIds.PaginatedListAsync(request.PagingParams);

			return paginatedComments.ToMappedPagedList<Comment, GetCommentModel>(Mapper);
		}
	}
}
