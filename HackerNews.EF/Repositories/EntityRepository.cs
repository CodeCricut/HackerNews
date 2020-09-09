﻿using HackerNews.Domain;
using HackerNews.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

		public async Task<EntityT> AddEntityAsync(EntityT entity)
		{
			var addedEntity = (await Task.Run(() => _context.Set<EntityT>().Add(entity))).Entity;
			return addedEntity;
		}

		public async Task<List<EntityT>> AddEntititesAsync(List<EntityT> entities)
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

		public async Task UpdateEntityAsync(int id, EntityT updatedEntity)
		{
			await Task.Run(() =>
			{
				updatedEntity.Id = id;
				_context.Entry(updatedEntity).State = EntityState.Modified;
			});
		}

		public async Task SoftDeleteEntityAsync(int id)
		{
			var entity = await _context.Set<EntityT>().FindAsync(id);
			entity.Deleted = true;
			await UpdateEntityAsync(id, entity);
		}

		public async Task<bool> SaveChangesAsync()
		{
			try
			{
				return (await _context.SaveChangesAsync()) > 0;
			}
			catch
			{
				return false;
			}
		}
	}
}
