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
	public class ArticleRepository : EntityRepository<Article>
	{
		public ArticleRepository(HackerNewsContext context) : base(context)
		{
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
