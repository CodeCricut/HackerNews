using HackerNews.Api.DB_Helpers;
using HackerNews.Api.Helpers.Objects;
using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters.Trimmers
{
	public static class Trimmer
	{
		/// <summary>
		/// Return a new article that is free of circular dependencies. The <paramref name="article"/> parameter 
		/// will be left untouched in order to prevent unintended modifications to the database.
		/// </summary>
		/// <param name="article">The article that the new article will look like (minus the children)</param>
		/// <param name="trimChildren">Trim all complex children, instead of just resolving circular dependencies by way
		/// of trimming parents on children.</param>
		/// <returns></returns>
		public static Article GetNewTrimmedArticle(Article article, bool trimChildren)
		{
			var newArticle = article.Copy();

			if (trimChildren) newArticle.Comments.Clear();
			else
			{
				for (int i = 0; i < newArticle.Comments.Count; i++)
				{
					var comment = newArticle.Comments[i];
					newArticle.Comments[i] = GetNewTrimmedComment(comment, trimParents: true, trimChildren: false);
				}
			}
			return newArticle;
		}

		public static async Task<List<Article>> GetNewTrimmedArticlesAsync(List<Article> articles, bool trimChildren)
		{
			var trimTask = TaskHelper.RunConcurrentFuncsAsync(articles, a => GetNewTrimmedArticle(a, trimChildren));
			return await trimTask;
		}

		/// <summary>
		/// Return a new commennt that is free of circular dependencies. The <paramref name="comment"/> parameter 
		/// will be left untouched in order to prevent unintended modifications to the database.
		/// </summary>
		/// <param name="comment">The comment that the new comment will look like (minus the children)</param>
		/// <param name="trimChildren">Trim all complex children, instead of just resolving circular dependencies by way
		/// of trimming parents on children.</param>
		public static Comment GetNewTrimmedComment(Comment comment, bool trimParents, bool trimChildren)
		{
			var newComment = comment.Copy();
			if (trimParents)
			{
				newComment.ParentArticle = null;
				newComment.ParentComment = null;
			}
			else
			{
				if (newComment.ParentArticle != null)
				{
					newComment.ParentArticle = GetNewTrimmedArticle(newComment.ParentArticle, true);
				}
				if (newComment.ParentComment != null)
				{
					newComment.ParentComment = GetNewTrimmedComment(newComment.ParentComment, trimParents: false, trimChildren: true);
				}
			}

			if (trimChildren)
			{
				newComment.Comments.Clear();
			}
			else
			{
				for (int i = 0; i < newComment.Comments.Count; i++)
				{
					// rename to childComment
					var childComment = newComment.Comments[i];
					newComment.Comments[i] = GetNewTrimmedComment(childComment, trimParents: true, trimChildren: false);
					// check to see if the assignment changed the actual comment reference
				}
			}

			return newComment;
		}

		public static async Task<List<Comment>> GetNewTrimmedCommentsAsync(List<Comment> comments, bool trimParents, bool trimChildren)
		{
			var trimTask = TaskHelper.RunConcurrentFuncsAsync(comments, c => GetNewTrimmedComment(c, trimParents, trimChildren));
			return await trimTask;
		}
	}
}
