using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories.Comments
{
	public class ReadCommentRepository : ReadEntityRepository<Comment>
	{
		public ReadCommentRepository(DbContext context) : base(context)
		{
		}

		public override Task<PagedList<Comment>> GetEntitiesByQueryAsync(string query, PagingParams pagingParams)
		{
			return Task.Factory.StartNew(() =>
			{
				var withoutChildren = _context.Set<Comment>().AsQueryable();
				var withChildren = IncludeChildren(withoutChildren);

				var matchingQuery = withChildren.Where(
					c =>
						c.Board.Title.Contains(query) ||
						c.Board.Description.Contains(query) ||
						c.User.Username.Contains(query) ||
						c.Text.Contains(query)
					);

				return PagedList<Comment>.Create(matchingQuery, pagingParams);
			});
		}

		public override IQueryable<Comment> IncludeChildren(IQueryable<Comment> queryable)
		{
			return queryable
				.Include(c => c.ChildComments)
					.Include(c => c.ParentArticle)
					.Include(c => c.ParentComment)
					.Include(a => a.UsersLiked)
					.Include(a => a.UsersDisliked);
		}
	}
}
