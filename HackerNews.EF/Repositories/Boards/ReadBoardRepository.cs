using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories.Boards
{
	public class ReadBoardRepository : ReadEntityRepository<Board>
	{
		public ReadBoardRepository(DbContext context) : base(context)
		{
		}

		public override Task<PagedList<Board>> GetEntitiesByQueryAsync(string query, PagingParams pagingParams)
		{
			return Task.Factory.StartNew(() =>
			{
				var withoutChildren = _context.Set<Board>().AsQueryable();
				var withChildren = IncludeChildren(withoutChildren);

				var matchingQuery = withChildren.Where(
					b => 
						b.Title.Contains(query) ||
						b.Description.Contains(query)
					);

				return PagedList<Board>.Create(matchingQuery, pagingParams);
			});
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
