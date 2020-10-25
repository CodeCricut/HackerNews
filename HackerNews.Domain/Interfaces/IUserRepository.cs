using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces
{
	public interface IUserRepository : IEntityRepository<User>
	{
		Task<User> SaveArticle(int articleId);
		Task<User> SaveComment(int commentId);
	}
}
