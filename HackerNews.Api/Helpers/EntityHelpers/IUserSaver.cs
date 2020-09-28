using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public interface IUserSaver
	{
		Task<User> SaveArticleToUserAsync(int userId, int articleId);
		Task<User> SaveCommentToUserAsync(int userId, int commentId);
	}
}
