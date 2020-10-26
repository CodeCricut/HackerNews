using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Articles;
using HackerNews.Infrastructure.Repository.Boards;
using HackerNews.Infrastructure.Repository.Comments;
using HackerNews.Infrastructure.Repository.Users;
using Microsoft.EntityFrameworkCore;

namespace HackerNews.Infrastructure.Repository.Common
{
	 class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _db;

		public IArticleRepository Articles { get; private set; }

		public IBoardRepository Boards { get; private set; }

		public ICommentRepository Comments { get; private set; }

		public IUserRepository Users { get; private set; }

		public UnitOfWork(DbContext db)
		{
			_db = db;

			Articles = new ArticleRepository(db);
			Boards = new BoardRepository(db);
			Comments = new CommentRepository(db);
			Users = new UserRepository(db);
		}

		public bool SaveChanges()
		{
			return _db.SaveChanges() > 0;
		}

		public void Dispose()
		{
			_db.Dispose();
		}
	}
}
