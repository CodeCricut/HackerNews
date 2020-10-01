using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public abstract class EntityRepository<EntityT> : IEntityRepository<EntityT>
		where EntityT : DomainEntity
	{
		protected readonly HackerNewsContext _context;

		public EntityRepository(HackerNewsContext context)
		{
			_context = context;
		}

		public virtual async Task<EntityT> AddEntityAsync(EntityT entity)
		{
			// delete try blcok
			try
			{
				var addedEntity = (await Task.Run(() => _context.Set<EntityT>().Add(entity))).Entity;
				return addedEntity;
			}
			catch (Exception e)
			{
				throw;
			}
		}

		public virtual async Task<List<EntityT>> AddEntititesAsync(List<EntityT> entities)
		{
			return await Task.Factory.StartNew(() =>
			{
				_context.Set<EntityT>().AddRange(entities);
				return entities;
			});
		}

		// need to be implemented on a per-entity bases in order to include all properties
		public abstract Task<EntityT> GetEntityAsync(int id);
		public abstract Task<IEnumerable<EntityT>> GetEntitiesAsync();

		public virtual async Task UpdateEntityAsync(int id, EntityT updatedEntity)
		{
			var local = _context.Set<EntityT>().Local.FirstOrDefault(x => x.Id == id);
			if (local != null) _context.Entry(local).State = EntityState.Detached;

			updatedEntity.Id = id;
			_context.Entry(updatedEntity).State = EntityState.Modified;
		}

		public virtual async Task SoftDeleteEntityAsync(int id)
		{
			var entity = await _context.Set<EntityT>().FindAsync(id);
			if (entity == null) throw new NotFoundException();
			entity.Deleted = true;
			await UpdateEntityAsync(id, entity);
		}

		public virtual async Task<bool> SaveChangesAsync()
		{
			try
			{
				return (await _context.SaveChangesAsync()) > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public virtual async Task<bool> VerifyExistsAsync(int id)
		{
			return (await GetEntityAsync(id)) != null;
		}
	}
}
