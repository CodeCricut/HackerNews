using HackerNews.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories
{
	public interface IEntityRepository<EntityT> where EntityT : DomainEntity
	{
		Task<EntityT> AddEntityAsync(EntityT entity);
		Task<List<EntityT>> AddEntititesAsync(List<EntityT> entities);
		Task<EntityT> GetEntityAsync(int id);
		Task<IEnumerable<EntityT>> GetEntitiesAsync();
		Task UpdateEntityAsync(int id, EntityT updatedEntity);
		Task SoftDeleteEntityAsync(int id);
		Task<bool> SaveChangesAsync();
		Task<bool> VerifyExistsAsync(int id);
	}
}
