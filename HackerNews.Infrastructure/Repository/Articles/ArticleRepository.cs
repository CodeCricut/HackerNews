using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Articles
{
	public class ArticleRepository : EntityRepository<Article>, IArticleRepository
	{
		public ArticleRepository(DbContext context) : base(context)
		{
		}

		public Task<Article> AddComment(int articleId, Comment comment)
		{
			throw new NotImplementedException();
		}

		public override Task<IQueryable<Article>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<Article>()
				.Include(a => a.Comments)
				.Include(a => a.UsersLiked)
				.Include(a => a.UsersDisliked)
				.AsQueryable()
				);
		}

		public override Task<Article> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<Article>()
				.Include(a => a.Comments)
				.Include(a => a.UsersLiked)
				.Include(a => a.UsersDisliked)
				.FirstOrDefault(a => a.Id == id)
				);
		}

		public async Task<Article> VoteArticle(int id)
		{
			// TODO
			throw new NotImplementedException();
		}

		public Task<Article> VoteArticle(int id, bool upvote)
		{
			throw new NotImplementedException();
		}
	}
}
