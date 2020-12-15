using System;

namespace HackerNews.Domain.Interfaces
{
	/// <summary>
	/// Encompasses a single context that can be used to interact with multiple entities/repositories without closing and reopening
	/// connections to the DB.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		IArticleRepository Articles { get; }
		IBoardRepository Boards { get; }
		ICommentRepository Comments { get; }
		IUserRepository Users { get; }
		IImageRepository Images { get; }

		bool SaveChanges();
	}
}
