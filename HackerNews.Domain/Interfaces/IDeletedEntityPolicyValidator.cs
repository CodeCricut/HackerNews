using HackerNews.Domain.Common;
using System.Linq;

namespace HackerNews.Domain.Interfaces
{
	/// <summary>
	/// Implements the validation behavior associated with each value in <see cref="DeletedEntityPolicy"/>. 
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IDeletedEntityPolicyValidator<TEntity> where TEntity : DomainEntity
	{
		/// <summary>
		/// Validate every <typeparamref name="TEntity"/> in <paramref name="entities"/>. If an entity cannot be validated
		/// (i.e. if the entity is deleted an not accessible according to the <paramref name="policy"/>), then it will
		/// simply not be included in the returned <see cref="IQueryable{TEntity}"/>.
		/// </summary>
		/// <param name="entities"></param>
		/// <param name="policy"></param>
		/// <returns></returns>
		IQueryable<TEntity> ValidateEntityQuerable(IQueryable<TEntity> entities, DeletedEntityPolicy policy);

		/// <summary>
		/// Validate the given <paramref name="entity"/>. If the entity cannot be validated (i.e. if the entity is deleted and not
		/// accessible according to the <paramref name="policy"/>), then it will throw a <see cref="Exceptions.EntityDeletedException"/>.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="policy"></param>
		/// <returns>The entity, which may have properties nullified if not accessible.</returns>
		TEntity ValidateEntity(TEntity entity, DeletedEntityPolicy policy);
	}
}
