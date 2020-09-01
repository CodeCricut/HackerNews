using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public interface IArticleCommentRepository
	{
		public Task<IEnumerable<Article>> GetArticlesWithChildrenAsync();
		public Task<Article> GetArticleWithChildrenAsync(int id);
		public Task<IEnumerable<Comment>> GetCommentsWithParentsAsync(bool includeChildren);
		public Task<Comment> GetCommentWithParentAsync(int id, bool includeChildren);
	}
}
