using HackerNews.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces
{
	public interface IEntityRepository<TEntity> where TEntity : DomainEntity
	{
		Task<TEntity> AddEntityAsync(TEntity entity);
		Task<IEnumerable<TEntity>> AddEntititesAsync(IEnumerable<TEntity> entities);
		Task<bool> UpdateEntityAsync(int id, TEntity updatedEntity);
		Task<bool> DeleteEntityAsync(int id);

		Task<TEntity> GetEntityAsync(int id);
		Task<IQueryable<TEntity>> GetEntitiesAsync();
		Task<bool> EntityExistsAsync(int id);
	}
}
