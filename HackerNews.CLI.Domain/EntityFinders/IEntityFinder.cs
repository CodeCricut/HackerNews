﻿using HackerNews.Domain.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public interface IEntityFinder<TEntity>
	{
		Task<PaginatedList<TEntity>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams);
		Task<PaginatedList<TEntity>> GetPageAsync(PagingParams pagingParams);
		Task<TEntity> GetByIdAsync(int id);
	}
}
