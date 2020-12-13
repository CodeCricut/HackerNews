using HackerNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HackerNews.EF
{
	public class HackerNewsContext : DbContext
	{
		public DbSet<Article> Articles { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Board> Boards { get; set; }
		//public DbSet<Image> Images { get; set; }

		// we will inject the configuration options in through the api/startup class
		public HackerNewsContext(DbContextOptions<HackerNewsContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}
	}
}
