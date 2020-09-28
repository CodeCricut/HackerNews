using HackerNews.Domain;
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
				);

			return users;
		}

		public override async Task<User> GetEntityAsync(int id)
		{
			return (await GetEntitiesAsync()).FirstOrDefault(u => u.Id == id);
		}
	}
}
