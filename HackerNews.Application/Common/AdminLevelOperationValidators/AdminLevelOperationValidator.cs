﻿using HackerNews.Domain.Common;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.AdminLevelOperationValidators
{
	public abstract class AdminLevelOperationValidator<TEntity> : IAdminLevelOperationValidator<TEntity> where TEntity : IDomainEntity
	{
		protected readonly IMediator _mediator;

		public AdminLevelOperationValidator(IMediator mediator)
		{
			_mediator = mediator;
		}

		public abstract Task<bool> CanDeleteEntityAsync(TEntity entity, AdminLevel adminLevel);

		public abstract Task<bool> CanModifyEntityAsync(TEntity entity, AdminLevel adminLevel);
	}
}
