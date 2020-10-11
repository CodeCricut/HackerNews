using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;

namespace HackerNews.EF.Repositories.Users
{
	public class WriteUserRepository : WriteEntityRepository<User>
	{
		public WriteUserRepository(DbContext context) : base(context)
		{
		}
	}
}
