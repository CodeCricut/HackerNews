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
				.Property(a => a.Text).IsRequired();
			modelBuilder.Entity<Article>()
				.Property(a => a.Title).IsRequired();
			modelBuilder.Entity<Article>()
				.Property(a => a.UserId).IsRequired();

			modelBuilder.Entity<Comment>()
				.Property(c => c.Text).IsRequired();
			modelBuilder.Entity<Comment>()
				.Property(c => c.UserId).IsRequired();

			modelBuilder.Entity<User>()
				.Property(u => u.FirstName).IsRequired();
			modelBuilder.Entity<User>()
				.Property(u => u.LastName).IsRequired();
			modelBuilder.Entity<User>()
				.Property(u => u.Username).IsRequired();
			modelBuilder.Entity<User>()
				.Property(u => u.Password).IsRequired();

			modelBuilder.Entity<Article>()
				.HasOne(a => a.User).WithMany(u => u.Articles);

			modelBuilder.Entity<Comment>()
				.HasOne(c => c.User).WithMany(u => u.Comments);

			modelBuilder.Entity<UserArticle>()
				.HasKey(ua => new { ua.UserId, ua.ArticleId });
			modelBuilder.Entity<UserArticle>()
				.HasOne(ua => ua.User)
				.WithMany(u => u.SavedArticles)
				.HasForeignKey(ua => ua.UserId);
			modelBuilder.Entity<UserArticle>()
				.HasOne(ua => ua.Article)
				.WithMany(a => a.UsersSaved)
				.HasForeignKey(ua => ua.ArticleId);

			modelBuilder.Entity<UserComment>()
				.HasKey(uc => new { uc.UserId, uc.CommentId });
			modelBuilder.Entity<UserComment>()
				.HasOne(uc => uc.User)
				.WithMany(u => u.SavedComments)
				.HasForeignKey(uc => uc.UserId);
			modelBuilder.Entity<UserComment>()
				.HasOne(uc => uc.Comment)
				.WithMany(a => a.UsersSaved)
				.HasForeignKey(uc => uc.CommentId);
		}
	}
}
