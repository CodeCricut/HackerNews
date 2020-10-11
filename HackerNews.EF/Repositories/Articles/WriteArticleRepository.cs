using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;

namespace HackerNews.EF.Repositories.Articles
{
	public class WriteArticleRepository : WriteEntityRepository<Article>
	{
		public WriteArticleRepository(DbContext context) : base(context)
		{
		}
	}
}
