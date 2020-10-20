using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories.Users
{
	public class ReadUserRepository : ReadEntityRepository<User>
	{
		public ReadUserRepository(DbContext context) : base(context)
		{
		}

		public override Task<PagedList<User>> GetEntitiesByQueryAsync(string query, PagingParams pagingParams)
		{
			return Task.Factory.StartNew(() =>
			{
				var withoutChildren = _context.Set<User>().AsQueryable();
				var withChildren = IncludeChildren(withoutChildren);

				var matchingQuery = withChildren.Where(
					u => u.Username.Contains(query));

				return PagedList<User>.Create(matchingQuery, pagingParams);
			});
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
