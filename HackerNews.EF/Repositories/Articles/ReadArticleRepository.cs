using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories.Articles
{
	public class ReadArticleRepository : ReadEntityRepository<Article>
	{
		public ReadArticleRepository(DbContext context) : base(context)
		{
		}

		public override Task<PagedList<Article>> GetEntitiesByQueryAsync(string query, PagingParams pagingParams)
		{
			return Task.Factory.StartNew(() =>
			{
				var withoutChildren = _context.Set<Article>().AsQueryable();
				var withChildren = IncludeChildren(withoutChildren);

				var matchingQuery = withChildren.Where(
					a => a.Title.Contains(query) ||
						a.Text.Contains(query) ||
						a.User.Username.Contains(query) ||
						a.Board.Title.Contains(query) ||
						a.Board.Description.Contains(query)
					);

				return PagedList<Article>.Create(matchingQuery, pagingParams);
			});
		}

		public override IQueryable<Article> IncludeChildren(IQueryable<Article> queryable)
		{
			return queryable
				.Include(a => a.Comments)
				.Include(a => a.UsersLiked)
				.Include(a => a.UsersDisliked);
		}
	}
}
