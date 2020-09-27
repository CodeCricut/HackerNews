using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;

namespace HackerNews.EF
{
	public class HackerNewsContext : DbContext
	{
		public DbSet<Article> Articles { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<User> Users { get; set; }

		// we will inject the configuration options in through the api/startup class
		public HackerNewsContext(DbContextOptions<HackerNewsContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Article>()
				.HasOne<User>().WithMany(u => u.Articles);

			modelBuilder.Entity<Comment>()
				.HasOne<User>().WithMany(u => u.Comments);
		}
	}
}
