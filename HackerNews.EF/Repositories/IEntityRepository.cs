using HackerNews.Domain;
using HackerNews.Domain.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories
{
	public interface IEntityRepository<EntityT> where EntityT : DomainEntity
	{
		Task<EntityT> AddEntityAsync(EntityT entity);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entities"></param>
		/// <returns>The added entities.</returns>
		Task<IEnumerable<EntityT>> AddEntititesAsync(List<EntityT> entities);
		Task<EntityT> GetEntityAsync(int id);
		Task<PagedList<EntityT>> GetEntitiesAsync(PagingParams pagingParams);
		Task<IEnumerable<EntityT>> GetEntitiesAsync(IEnumerable<int> ids);
		Task UpdateEntityAsync(int id, EntityT updatedEntity);
		Task SoftDeleteEntityAsync(int id);
		Task<bool> SaveChangesAsync();
		Task<bool> VerifyExistsAsync(int id);
		IQueryable<EntityT> IncludeChildren(IQueryable<EntityT> queryable);
	}
}
