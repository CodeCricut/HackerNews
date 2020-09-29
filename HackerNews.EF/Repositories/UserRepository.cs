﻿using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories
{
	public class UserRepository : EntityRepository<User>
	{
		public UserRepository(HackerNewsContext context) : base(context)
		{
		}

		public override async Task<IEnumerable<User>> GetEntitiesAsync()
		{
			var users = await Task.Factory.StartNew(() => 
				_context.Users
				.Include(u => u.Articles)
				.Include(u => u.Comments)
				.Include(u => u.SavedArticles)
				.Include(u => u.SavedComments)
				.Include(u => u.LikedArticles)
				.Include(u => u.LikedComments)
				);

			return users;
		}

		public override async Task<User> GetEntityAsync(int id)
		{
			return (await GetEntitiesAsync()).FirstOrDefault(u => u.Id == id);
		}

		public override async Task<User> AddEntityAsync(User entity)
		{
			var currentDate = DateTime.UtcNow;
			entity.JoinDate = currentDate;

			var addedEntity = (await Task.Run(() => _context.Set<User>().Add(entity))).Entity;
			return addedEntity;
		}
	}
}
