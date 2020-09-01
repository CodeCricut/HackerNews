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
			// we don't want to actually delete articles. Instead, we just modify the deleted property.
			var article = _context.Articles.Find(id);
			article.Deleted = true;
			UpdateArticle(id, article);
		}

		public async Task<Article> GetArticleWithoutChildrenAsync(int id)
		{
			var article = await _context.Articles
					.SingleOrDefaultAsync(a => a.Id == id);
			// this is removing all comments from the article in the repo TODO: very bad
				article.Comments = null;
				return article;
		}

		// just... bad async
		public  Task<IEnumerable<Article>> GetArticlesWithoutChildrenAsync()
		{
			var articles =  _context.Articles.ToList();
			
				for(int i = 0; i < articles.Count; i++)
				{
				articles[i].Comments = null;
				}

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
