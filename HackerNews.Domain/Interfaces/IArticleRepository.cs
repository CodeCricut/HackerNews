using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces
{
	public interface IArticleRepository : IEntityRepository<Article>
	{
		Task<Article> VoteArticle(int id, bool upvote);
		Task<Article> AddComment(int articleId, Comment comment);
	}
}
