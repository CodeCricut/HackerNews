using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public class ArticleRepository : IArticleRepository
	{
		private readonly HackerNewsContext _context;

		public ArticleRepository(HackerNewsContext context)
		{
			_context = context;
		}

		#region Create
		public async Task<Article> AddArticleAsync(Article article)
		{
			var addedArticle = await Task.Run(() => (_context.Articles.Add(article)).Entity);
			return addedArticle;
		}

		public async Task<List<Article>> AddArticlesAsync(List<Article> articles)
		{
			return await Task.Factory.StartNew(() =>
			{
				_context.Articles.AddRange(articles);
				return articles;
			});
		}
		#endregion

		#region Read
		public async Task<Article> GetArticleAsync(int id)
		{
			var article = await _context.Articles
					.Include(a => a.Comments)
					.SingleOrDefaultAsync(a => a.Id == id);

			return article;
		}

		public async Task<IEnumerable<Article>> GetArticlesAsync()
		{
			var articles = await Task.Factory.StartNew(() =>
				_context.Articles
					.Include(a => a.Comments));

			return articles;
		}
		#endregion

		#region Update
		public async Task UpdateArticleAsync(int id, Article updatedArticle)
		{
			try
			{
				await Task.Run(() =>
				{
					updatedArticle.Id = id;
					_context.Entry(updatedArticle).State = EntityState.Modified;
				});
			}
			catch (Exception e)
			{
				throw;
			}
		}
		#endregion

		#region Delete
		public async Task DeleteArticleAsync(int id)
		{
			// We don't want to actually delete articles. Instead, we just modify the deleted property.
			var article = await _context.Articles.FindAsync(id);
			article.Deleted = true;
			await UpdateArticleAsync(id, article);
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
