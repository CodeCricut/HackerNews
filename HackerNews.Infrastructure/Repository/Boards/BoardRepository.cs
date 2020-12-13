using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Boards
{
	class BoardRepository : EntityRepository<Board>, IBoardRepository
	{
		public BoardRepository(DbContext context) : base(context)
		{
		}

		public override Task<IQueryable<Board>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<Board>()
					.Include(b => b.Articles)
					.Include(b => b.Comments)
					.Include(b => b.Creator)
					.Include(b => b.Moderators)
					.Include(b => b.Subscribers)
					//.Include(b => b.BoardImage)
					.AsQueryable()
					);
		}

		public override Task<Board> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<Board>()
					.Include(b => b.Articles)
					.Include(b => b.Comments)
					.Include(b => b.Creator)
					.Include(b => b.Moderators)
					.Include(b => b.Subscribers)
					//.Include(b => b.BoardImage)
					.FirstOrDefault(board => board.Id == id)
					);
		}
	}
}
