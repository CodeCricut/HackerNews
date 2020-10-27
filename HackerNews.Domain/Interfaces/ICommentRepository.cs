using HackerNews.Domain.Entities;

namespace HackerNews.Domain.Interfaces
{
	public interface ICommentRepository : IEntityRepository<Comment>
	{
	}
}
