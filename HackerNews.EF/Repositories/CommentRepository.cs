using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HackerNews.EF
{
	public class CommentRepository : EntityRepository<Comment>
	{
		public CommentRepository(HackerNewsContext context) : base(context)
		{
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
