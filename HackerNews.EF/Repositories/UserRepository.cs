using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories
{
	public class UserRepository : EntityRepository<User>, IUserRepository
	{
		public UserRepository(HackerNewsContext context) : base(context)
		{
		}

		public async Task<User> GetUserByCredentialsAsync(string username, string password)
		{
			return await Task.Factory.StartNew(() =>
			{
				var withoutChildren = _context.Users.AsQueryable();
				var withChildren = IncludeChildren(withoutChildren);

				return withChildren.FirstOrDefault(u => u.Username == username && u.Password == password);
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
				.Include(u => u.LikedComments);
		}
	}
}
