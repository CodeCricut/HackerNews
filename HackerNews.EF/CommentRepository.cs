using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public class CommentRepository : ICommentRepository
	{
		private readonly HackerNewsContext _context;

		public CommentRepository(HackerNewsContext context)
		{
			_context = context;
		}

		#region Create
		public async Task<Comment> AddCommentAsync(Comment comment)
		{
			Comment addedComment = null;
			await Task.Run(() => {
				addedComment = _context.Comments.Add(comment).Entity;
			});
			return addedComment;
		}

		public async Task AddCommentsAsync(List<Comment> comments)
		{
			await Task.Run(() => _context.Comments.AddRange(comments));
		}
		#endregion

		#region Read
		public async Task<Comment> GetCommentAsync(int id, bool includeChildren)
		{
			var comment = await _context.Comments
				.Include(c => c.ParentComment)
				.Include(c => c.Comments)
				.Include(c => c.ParentArticle)
				.SingleOrDefaultAsync(c => c.Id == id);

			return comment;
		}

		public async Task<IEnumerable<Comment>> GetCommentsAsync(bool includeChildren)
		{
			var comments = await _context.Comments
				.Include(c => c.Comments)
				.Include(c => c.ParentArticle)
				.Include(c => c.ParentComment)
					.ToListAsync();

			return comments;

		}
		#endregion

		#region Update
		public async Task UpdateCommentAsync(int id, Comment updatedComment)
		{
			try
			{
				await Task.Run(() =>
				{
					updatedComment.Id = id;
					_context.Entry(updatedComment).State = EntityState.Modified;
				});
			}
			catch (Exception e)
			{
				throw;
			}
		}
		#endregion

		#region Delete
		public async Task DeleteCommentAsync(int id)
		{
			// We don't want to actually delete comments. Instead, we just modify the deleted property.
			var comment = await _context.Comments.FindAsync(id);
			comment.Deleted = true;
			await UpdateCommentAsync(id, comment);
		}
		#endregion

		#region Save
		public async Task<bool> SaveChangesAsync()
		{
			try
			{
				return (await _context.SaveChangesAsync()) > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}
		#endregion
	}
}
