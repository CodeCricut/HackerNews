using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public interface ICommentRepository
	{
		/// <summary>
		/// Add a comment as is.
		/// </summary>
		/// <param name="Comment"></param>
		Task AddCommentAsync(Comment Comment);

		/// <summary>
		/// Get all comments including their children. Children will reference parents, so circular 
		/// dependendies must be resolved downstream before presenting in JSON, for example.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Comment>> GetCommentsAsync(bool includeChildren);

		/// <summary>
		/// Get a comment including its children. Children will reference parents, so circular 
		/// dependendies must be resolved downstream before presenting in JSON, for example.
		/// </summary>
		/// <returns></returns>
		Task<Comment> GetCommentAsync(int id, bool includeChildren);

		/// <summary>
		/// Update the comment with the given <paramref name="id"/> to match the <paramref name="updatedComment"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updatedComment"></param>
		Task UpdateCommentAsync(int id, Comment updatedComment);

		/// <summary>
		/// Mark the <see cref="Comment.Deleted"/> property as true. Will not actually delete the comment from the database.
		/// </summary>
		/// <param name="id"></param>
		Task DeleteCommentAsync(int id);

		/// <summary>
		/// Propogate changes throughout the database. Will save for the entire context, not just comments.
		/// </summary>
		/// <returns>Successful or not</returns>
		Task<bool> SaveChangesAsync();
	}
}
