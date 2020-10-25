﻿using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Articles
{
	public class ArticleRepository : EntityRepository<Article>, IArticleRepository
	{
		public ArticleRepository(DbContext context) : base(context)
		{
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
	}
}
