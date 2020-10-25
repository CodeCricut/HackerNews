using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces
{
	public interface IBoardRepository : IEntityRepository<Board>
	{
		Task<Board> AddBoardSubscriberAsync(int boardId);
		Task<Board> AddBoardModeratorAsync(int boardId, int moderatorId);
	}
}
