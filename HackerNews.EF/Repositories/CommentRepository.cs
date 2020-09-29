﻿using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
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

		public override async Task<IEnumerable<Comment>> GetEntitiesAsync()
		{
			return await Task.Factory.StartNew(() => _context.Comments
					.Include(c => c.ChildComments)
					.Include(c => c.ParentArticle)
					.Include(c => c.ParentComment)
					.Include(a => a.UsersLiked)
					.Include(a => a.UsersDisliked)
					);
		}

		public override async Task<Comment> GetEntityAsync(int id)
		{
			return (await GetEntitiesAsync()).FirstOrDefault(c => c.Id == id);
		}
	}
}
