using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IArticleRepository Articles { get; }
		IBoardRepository Boards { get; }
		ICommentRepository Comments { get; }
		IUserRepository Users { get; }

		bool SaveChanges();
	}
}
