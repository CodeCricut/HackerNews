using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public interface IArticleRepository
	{
		void AddArticle(Article article);
		Task<IEnumerable<Article>> GetArticlesWithoutChildrenAsync();
		Task<Article> GetArticleWithoutChildrenAsync(int id);
		void UpdateArticle(int id, Article updatedArticle);
		void DeleteArticle(int id);
		/// <summary>
		/// 
		/// </summary>
		/// <returns>Successful or not</returns>
		Task<bool> SaveChangesAsync();
	}
}
