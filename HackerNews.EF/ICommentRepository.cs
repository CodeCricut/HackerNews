using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public interface ICommentRepository
	{
		void AddComment(Comment Comment);
		Task<IEnumerable<Comment>> GetCommentsWithoutParentAsync(bool includeChildren);
		Task<Comment> GetCommentWithoutParentAsync(int id, bool includeChildren);
		void UpdateComment(int id, Comment updatedComment);
		void DeleteComment(int id);
		/// <summary>
		/// 
		/// </summary>
		/// <returns>Successful or not</returns>
		Task<bool> SaveChangesAsync();
	}
}
