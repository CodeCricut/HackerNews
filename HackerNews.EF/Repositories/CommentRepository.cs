using HackerNews.Domain;
using HackerNews.Domain.Helpers;
using HackerNews.Domain.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
