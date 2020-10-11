using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HackerNews.EF.Repositories.Users
{
	public class ReadUserRepository : ReadEntityRepository<User>
	{
		public ReadUserRepository(DbContext context) : base(context)
		{
		}

		public override IQueryable<User> IncludeChildren(IQueryable<User> queryable)
		{
			return queryable
				.Include(u => u.Articles)
				.Include(u => u.Comments)
				.Include(u => u.SavedArticles)
				.Include(u => u.SavedComments)
				.Include(u => u.LikedArticles)
				.Include(u => u.LikedComments)
				.Include(u => u.BoardsModerating)
				.Include(u => u.BoardsSubscribed);
		}
	}
}
