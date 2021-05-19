using HackerNews.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public interface IGetEntityRepository<TEntity>
	{
		Task<PaginatedList<TEntity>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams);
		Task<PaginatedList<TEntity>> GetPageAsync(PagingParams pagingParams);
		Task<TEntity> GetByIdAsync(int id);
	}
}
