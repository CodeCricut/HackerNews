using HackerNews.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces
{
	/// <summary>
	/// The base interface for interacting with <typeparamref name="TEntity"/>s in the database.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IEntityRepository<TEntity> where TEntity : class, IDomainEntity
	{
		/// <summary>
		/// Add the <paramref name="entity"/> to the database then return it. Returned entities will have properties set by 
		/// the ORM, such as IDs, populated.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<TEntity> AddEntityAsync(TEntity entity);
		/// <summary>
		/// Same as <see cref="AddEntityAsync(TEntity)"/>, except it works for multiple entities at once. Preferable
		/// when many entities need to be added at once, as all can be done so asynchronously.
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		Task<IEnumerable<TEntity>> AddEntititesAsync(IEnumerable<TEntity> entities);

		/// <summary>
		/// Update the <typeparamref name="TEntity"/> with the given <paramref name="id"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updatedEntity"></param>
		/// <returns>Successful</returns>
		Task<bool> UpdateEntityAsync(int id, TEntity updatedEntity);

		/// <summary>
		/// Soft delete (set the "Deleted" property to true) the given entity.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Successful</returns>
		Task<bool> DeleteEntityAsync(int id);

		/// <summary>
		/// Get the entity with the given <paramref name="id"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<TEntity> GetEntityAsync(int id);

		/// <summary>
		/// Get all <typeparamref name="TEntity"/>s in the database (as a <see cref="IQueryable{TEntity}"/>, which won't actually
		/// fetch the entities until the result is enumerated.
		/// </summary>
		/// <returns></returns>
		Task<IQueryable<TEntity>> GetEntitiesAsync();
		/// <summary>
		/// Verify that a <typeparamref name="TEntity"/> with the given <paramref name="id"/> exists.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>The entity exists.</returns>
		Task<bool> EntityExistsAsync(int id);
	}
}
