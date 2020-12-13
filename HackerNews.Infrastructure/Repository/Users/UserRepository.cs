using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Users
{
	class UserRepository : EntityRepository<User>, IUserRepository
	{
		public UserRepository(DbContext context) : base(context)
		{
		}

		public override Task<IQueryable<User>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<User>()
					.Include(u => u.Articles)
					.Include(u => u.Comments)
					.Include(u => u.SavedArticles)
					.Include(u => u.SavedComments)
					.Include(u => u.LikedArticles)
					.Include(u => u.LikedComments)
					.Include(u => u.BoardsModerating)
					.Include(u => u.BoardsSubscribed)
					//.Include(u => u.ProfileImage)
					.AsQueryable()
				);
		}

		public override Task<User> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<User>()
					.Include(u => u.Articles)
					.Include(u => u.Comments)
					.Include(u => u.SavedArticles)
					.Include(u => u.SavedComments)
					.Include(u => u.LikedArticles)
					.Include(u => u.LikedComments)
					.Include(u => u.BoardsModerating)
					.Include(u => u.BoardsSubscribed)
					//.Include(u => u.ProfileImage)
					.FirstOrDefault(user => user.Id == id)
				);
		}
	}
}
