using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HackerNews.EF.Repositories.Boards
{
	public class ReadBoardRepository : ReadEntityRepository<Board>
	{
		public ReadBoardRepository(DbContext context) : base(context)
		{
		}

		public override IQueryable<Board> IncludeChildren(IQueryable<Board> queryable)
		{
			return queryable
				.Include(b => b.Articles)
				.Include(b => b.Comments)
				.Include(b => b.Creator)
				.Include(b => b.Moderators)
				.Include(b => b.Subscribers);
		}
	}
}
