using HackerNews.Domain.Common;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces
{
	public interface IAdminLevelOperationValidator<TEntity> where TEntity : IDomainEntity
	{
		Task<bool> CanDeleteEntityAsync(TEntity entity, AdminLevel adminLevel);
		Task<bool> CanModifyEntityAsync(TEntity entity, AdminLevel adminLevel);
	}
}
