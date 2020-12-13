using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Articles
{
	class ArticleRepository : EntityRepository<Article>, IArticleRepository
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
				.Include(a => a.AssociatedImage)
				.AsQueryable()
				);
		}

		public override Task<Article> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<Article>()
				.Include(a => a.Board)
				.Include(a => a.User)
				.Include(a => a.Comments)
				.Include(a => a.UsersLiked)
				.Include(a => a.UsersDisliked)
				.Include(a => a.AssociatedImage)
				.FirstOrDefault(a => a.Id == id)
				);
		}
	}
}
