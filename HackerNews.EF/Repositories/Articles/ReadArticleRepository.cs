using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HackerNews.EF.Repositories.Articles
{
	public class ReadArticleRepository : ReadEntityRepository<Article>
	{
		public ReadArticleRepository(DbContext context) : base(context)
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
