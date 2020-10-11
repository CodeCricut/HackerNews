using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories.Users
{
	public class UserLoginRepository : IUserLoginRepository
	{
		private readonly DbContext _context;
		private readonly IReadEntityRepository<User> _readUserRepo;

		public UserLoginRepository(DbContext context, IReadEntityRepository<User> readUserRepo)
		{
			_context = context;
			_readUserRepo = readUserRepo;
		}

		public async Task<User> GetUserByCredentialsAsync(string username, string password)
		{
			return await Task.Factory.StartNew(() =>
			{
				var withoutChildren = _context.Set<User>().AsQueryable();
				var withChildren = _readUserRepo.IncludeChildren(withoutChildren);

				return withChildren.FirstOrDefault(u => u.Username == username && u.Password == password);
			});
		}
	}
}
