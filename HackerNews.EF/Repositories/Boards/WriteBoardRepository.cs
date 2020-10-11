using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;

namespace HackerNews.EF.Repositories.Boards
{
	public class WriteBoardRepository : WriteEntityRepository<Board>
	{
		public WriteBoardRepository(DbContext context) : base(context)
		{
		}
	}
}
