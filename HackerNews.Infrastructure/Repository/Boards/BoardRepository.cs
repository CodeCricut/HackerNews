﻿using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Boards
{
	public class BoardRepository : EntityRepository<Board>, IBoardRepository
	{
		public BoardRepository(DbContext context) : base(context)
		{
		}

		public override Task<IQueryable<Board>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<Board>()
					.Include(b => b.Articles)
					.Include(b => b.Comments)
					.Include(b => b.Creator)
					.Include(b => b.Moderators)
					.Include(b => b.Subscribers)
					.AsQueryable()
					);
		}

		public override Task<Board> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<Board>()
					.Include(b => b.Articles)
					.Include(b => b.Comments)
					.Include(b => b.Creator)
					.Include(b => b.Moderators)
					.Include(b => b.Subscribers)
					.FirstOrDefault(board => board.Id == id)
					);
		}
	}
}