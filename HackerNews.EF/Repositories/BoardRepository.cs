using HackerNews.Domain;
using HackerNews.Domain.Helpers;
using HackerNews.Domain.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories
{
	public class BoardRepository : EntityRepository<Board>
	{
		public BoardRepository(HackerNewsContext context) : base(context)
		{
		}

		public override IQueryable<Board> IncludeChildren(IQueryable<Board> queryable)
		{
			return queryable
				.Include(b => b.Articles)
				.Include(b => b.Creator)
				.Include(b => b.Moderators)
				.Include(b => b.Subscribers);
		}
	}
}
