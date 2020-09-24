using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
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
					.Include(a => a.Comments));
		}

		public override async Task<Article> GetEntityAsync(int id)
		{
			return (await GetEntitiesAsync()).FirstOrDefault(a => a.Id == id);
		}
	}
}
