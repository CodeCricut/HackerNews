using HackerNews.Api.DB_Helpers;
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
			if (trimChildren) article.Comments = null;
			else
			{
				for (int i = 0; i < article.Comments.Count; i++)
				{
					var comment = article.Comments[i];
					comment = GetNewTrimmedComment(comment, trimParents: false, trimChildren: false);
				}
			}
			return article;
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
			if (trimParents)
			{
				comment.ParentArticle = null;
				comment.ParentComment = null;
			}
			else
			{
				if (comment.ParentArticle != null)
				{
					/////////
					comment.ParentArticle = GetNewTrimmedArticle(comment.ParentArticle, false);
				}
			}

			if (trimChildren)
			{
				comment.Comments = null;
			}
			else
			{
				for (int i = 0; i < comment.Comments.Count; i++)
				{
					// rename to childComment
					var childComment = comment.Comments[i];
					childComment = GetNewTrimmedComment(childComment, trimParents: true, trimChildren: false);
					// check to see if the assignment changed the actual comment reference
				}
			}

			return comment;
		}

		public static async Task<List<Comment>> GetNewTrimmedCommentsAsync(List<Comment> comments, bool trimParents, bool trimChildren)
		{
			var trimTask = TaskHelper.RunConcurrentFuncsAsync(comments, c => GetNewTrimmedComment(c, trimParents, trimChildren));
			return await trimTask;
		}
	}
}
