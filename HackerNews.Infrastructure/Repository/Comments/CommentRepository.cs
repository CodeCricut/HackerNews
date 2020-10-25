using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Comments
{
	public class CommentRepository : EntityRepository<Comment>, ICommentRepository
	{
		public CommentRepository(DbContext context) : base(context)
		{
		}

		public Task<Comment> AddSubComment(int parentId, Comment comment)
		{
			// TODO:
			throw new NotImplementedException();
		}

		public override Task<IQueryable<Comment>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<Comment>()
					.Include(c => c.ChildComments)
					.Include(c => c.ParentArticle)
					.Include(c => c.ParentComment)
					.Include(a => a.UsersLiked)
					.Include(a => a.UsersDisliked)
					.AsQueryable()
				);
		}

		public override Task<Comment> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<Comment>()
					.Include(c => c.ChildComments)
					.Include(c => c.ParentArticle)
					.Include(c => c.ParentComment)
					.Include(a => a.UsersLiked)
					.Include(a => a.UsersDisliked)
					.FirstOrDefault(comment => comment.Id == id)
				);
		}

		public Task<Comment> VoteComment(int id)
		{
			// TODO
			throw new NotImplementedException();
		}

		public Task<Comment> VoteComment(int id, bool upvote)
		{
			throw new NotImplementedException();
		}
	}
}
