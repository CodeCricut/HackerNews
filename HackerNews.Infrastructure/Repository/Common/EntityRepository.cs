using HackerNews.Domain.Common;
using HackerNews.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Common
{
	 abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : DomainEntity
	{
		protected readonly DbContext _context;

		public EntityRepository(DbContext context)
		{
			_context = context;
		}

		public virtual Task<IEnumerable<TEntity>> AddEntititesAsync(IEnumerable<TEntity> entities)
		{
			return Task.Factory.StartNew(() =>
			{
				_context.Set<TEntity>().AddRange(entities);
				return entities;
			});
		}

		public virtual Task<TEntity> AddEntityAsync(TEntity entity)
		{
			return Task.FromResult(_context.Set<TEntity>().Add(entity).Entity);
		}

		public virtual async Task<bool> DeleteEntityAsync(int id)
		{
			try
			{
				var entity = await _context.Set<TEntity>().FindAsync(id);
				entity.Deleted = true;
				await UpdateEntityAsync(id, entity);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public virtual async Task<bool> EntityExistsAsync(int id)
		{
			return (await GetEntityAsync(id)) != null;
		}

		public abstract Task<IQueryable<TEntity>> GetEntitiesAsync();

		public abstract Task<TEntity> GetEntityAsync(int id);

		public virtual Task<bool> UpdateEntityAsync(int id, TEntity updatedEntity)
		{
			return Task.Factory.StartNew(() =>
			{
				try
				{
					var local = _context.Set<TEntity>().Local.FirstOrDefault(x => x.Id == id);
					if (local != null) _context.Entry(local).State = EntityState.Detached;

					updatedEntity.Id = id;
					_context.Entry(updatedEntity).State = EntityState.Modified;

					return true;
				}
				catch (Exception ex)
				{
					return false;
				}
			});
		}
	}
}
