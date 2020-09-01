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

		public void AddComment(Comment comment)
		{
			_context.Comments.Add(comment);
		}

		public void DeleteComment(int id)
		{
			// we don't want to actually delete comments. Instead, we just modify the deleted property.
			var comment = _context.Comments.Find(id);
			comment.Deleted = true;
			UpdateComment(id, comment);
		}


		public void UpdateComment(int id, Comment updatedComment)
		{
			try
			{
				updatedComment.Id = id;
				_context.Entry(updatedComment).State = EntityState.Modified;
			}
			catch (Exception e)
			{
				throw;
			}
		}

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

	

		public async Task<IEnumerable<Comment>> GetCommentsWithoutParentAsync(bool includeChildren)
		{
			var comments =  _context.Comments
					.ToList();

			for(int i = 0; i < comments.Count; i++)
			{
				comments[i] = await GetCommentWithoutParentAsync(comments[i].Id, includeChildren);
			}

			return comments;
			
		}

		public async Task<Comment> GetCommentWithoutParentAsync(int id, bool includeChildren)
		{
			var comment = await _context.Comments
				// .Include(c => c.ParentComment)
				.Include(c => c.Comments)
				.SingleOrDefaultAsync(c => c.Id == id);
			if (includeChildren)
			{
				for(int i = 0; i < comment.Comments.Count; i++)
				{
					comment.Comments[i] = 
						await GetCommentWithoutParentAsync(comment.Comments[i].Id, includeChildren: true);
				}
			} else
			{
				comment.Comments = null;
			}

			// just in case...
			comment.ParentArticle = null;
			comment.ParentComment = null;

			return comment;
		}

	
		/*
		public Task<IEnumerable<Comment>> GetArticlesComments(int articleId)
		{
			return Task.Factory.StartNew(() => _context.Comments.Where(c => c.ParentArticle.Id == articleId)
			.Select(c => new Comment { 
				AuthorName = c.AuthorName,
				Deleted = c.Deleted,
				Id = c.Id,
				Karma = c.Karma,
				Text = c.Text,
				Url = c.Url
			}).AsEnumerable());
		}

		public Task<IEnumerable<Comment>> GetCommentsComments(int commentId)
		{
			return Task.Factory.StartNew(() => _context.Comments.Where(c => c.ParentComment.Id == commentId)
			.Select(c => new Comment
			{
				AuthorName = c.AuthorName,
				Deleted = c.Deleted,
				Id = c.Id,
				Karma = c.Karma,
				Text = c.Text,
				Url = c.Url
			}).AsEnumerable());
		}
		*/
	}
}
