using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HackerNews.EF
{
	public class ArticleRepository : EntityRepository<Article>
	{
		public ArticleRepository(HackerNewsContext context) : base(context)
		{
		}

		public override IQueryable<Article> IncludeChildren(IQueryable<Article> queryable)
		{
			return queryable
				.Include(a => a.Comments)
				.Include(a => a.UsersLiked)
				.Include(a => a.UsersDisliked);
		}
	}
}
