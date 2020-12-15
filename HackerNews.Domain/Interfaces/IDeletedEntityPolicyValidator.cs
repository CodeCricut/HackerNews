using HackerNews.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.Domain.Interfaces
{
	public interface IDeletedEntityPolicyValidator<TEntity> where TEntity : DomainEntity
	{
		IQueryable<TEntity> ValidateEntityQuerable(IQueryable<TEntity> entities, DeletedEntityPolicy policy);
		TEntity ValidateEntity(TEntity entity, DeletedEntityPolicy policy);
	}
}
