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

		public void AddArticle(Article article)
		{
			_context.Articles.Add(article);
		}

		public void DeleteArticle(int id)
		{
			// We don't want to actually delete articles. Instead, we just modify the deleted property.
			var article = _context.Articles.Find(id);
			article.Deleted = true;
			UpdateArticle(id, article);
		}

		public async Task<Article> GetArticleAsync(int id)
		{
			var article = await _context.Articles
					.Include(a => a.Comments)
					.SingleOrDefaultAsync(a => a.Id == id);
			
			return article;
		}

		// just... bad async
		public Task<IEnumerable<Article>> GetArticlesAsync()
		{
			var articles = _context.Articles
				.Include(a => a.Comments)
				.ToList();

			return Task.Factory.StartNew(() => articles.AsEnumerable());
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

		public void UpdateArticle(int id, Article updatedArticle)
		{
			try
			{
				updatedArticle.Id = id;
				_context.Entry(updatedArticle).State = EntityState.Modified;

			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}
