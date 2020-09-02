using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public interface IArticleRepository
	{
		/// <summary>
		/// Add an article as is.
		/// </summary>
		/// <param name="article"></param>
		Task<Article> AddArticleAsync(Article article);

		/// <summary>
		/// Get all articles including their children. Children will reference parents, so circular 
		/// dependendies must be resolved downstream before presenting in JSON, for example.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Article>> GetArticlesAsync();

		/// <summary>
		/// Get an article including its children. Children will reference parents, so circular 
		/// dependendies must be resolved downstream before presenting in JSON, for example.
		/// </summary>
		/// <returns></returns>
		Task<Article> GetArticleAsync(int id);

		/// <summary>
		/// Update the article with the given <paramref name="id"/> to match the <paramref name="updatedArticle"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updatedArticle"></param>
		Task UpdateArticleAsync(int id, Article updatedArticle);

		/// <summary>
		/// Mark the <see cref="Article.Deleted"/> property as true. Will not actually delete the article from the database.
		/// </summary>
		/// <param name="id"></param>
		Task DeleteArticleAsync(int id);

		/// <summary>
		/// Propogate changes throughout the database. Will save for the entire context, not just articles.
		/// </summary>
		/// <returns>Successful or not</returns>
		Task<bool> SaveChangesAsync();
	}
}
