using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Comments
{
	class CommentRepository : EntityRepository<Comment>, ICommentRepository
	{
		public CommentRepository(DbContext context) : base(context)
		{
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
	}
}
