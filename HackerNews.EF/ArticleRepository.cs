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

		public async Task<Article> AddArticleAsync(Article article)
		{
			var addedArticle = await Task.Run(() => (_context.Articles.Add(article)).Entity);
			return addedArticle;
		}

		public async Task<List<Article>> AddArticlesAsync(List<Article> articles)
		{
			_context.Articles.AddRange(articles);
			//List<Task<Article>> tasks = new List<Task<Article>>();

			//foreach(var article in articles)
			//{
			//	tasks.Add(AddArticleAsync(article));
			//}

			//return (await Task.WhenAll(tasks)).ToList();
			return articles;
		}

		public async Task DeleteArticleAsync(int id)
		{
			// We don't want to actually delete articles. Instead, we just modify the deleted property.
			var article = await _context.Articles.FindAsync(id);
			article.Deleted = true;
			await UpdateArticleAsync(id, article);
		}

		public async Task<Article> GetArticleAsync(int id)
		{
			var article = await _context.Articles
					.Include(a => a.Comments)
					.SingleOrDefaultAsync(a => a.Id == id);
			
			return article;
		}

		public async Task<IEnumerable<Article>> GetArticlesAsync(Func<Article, bool> articleFilter)
		{
			var articles = await Task.Factory.StartNew(() => 
			_context.Articles
				.Include(a => a.Comments)
				.Where(articleFilter));

			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticlesAsync()
		{
			var articles = await GetArticlesAsync(a => true);

			return articles;
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

	}
}
