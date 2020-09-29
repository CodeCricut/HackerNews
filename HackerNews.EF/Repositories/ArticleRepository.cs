using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public class ArticleRepository : EntityRepository<Article>
	{
		public ArticleRepository(HackerNewsContext context) : base(context)
		{
		}

		public override async Task<IEnumerable<Article>> GetEntitiesAsync()
		{
			return await Task.Factory.StartNew(() => _context.Articles
					.Include(a => a.Comments)
					.Include(a => a.UsersLiked)
					.Include(a => a.UsersDisliked)
					);
		}

		public override async Task<Article> GetEntityAsync(int id)
		{
			return (await GetEntitiesAsync()).FirstOrDefault(a => a.Id == id);
		}

		public override async Task<Article> AddEntityAsync(Article entity)
		{
			var currentDate = DateTime.UtcNow;
			entity.PostDate = currentDate;

			var addedEntity = (await Task.Run(() => _context.Set<Article>().Add(entity))).Entity;
			return addedEntity;
		}
	}
}
