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

			modelBuilder.Entity<UserArticleLikes>()
				.HasKey(ual => new { ual.UserId, ual.ArticleId });
			modelBuilder.Entity<UserArticleLikes>()
				.HasOne(ual => ual.User)
				.WithMany(u => u.LikedArticles)
				.HasForeignKey(ual => ual.UserId);
			modelBuilder.Entity<UserArticleLikes>()
				.HasOne(ual => ual.Article)
				.WithMany(a => a.UsersLiked)
				.HasForeignKey(ual => ual.ArticleId);

			modelBuilder.Entity<UserArticleDislikes>()
				.HasKey(uad => new { uad.UserId, uad.ArticleId });
			modelBuilder.Entity<UserArticleDislikes>()
				.HasOne(uad => uad.User)
				.WithMany(u => u.DislikedArticles)
				.HasForeignKey(uad => uad.UserId);
			modelBuilder.Entity<UserArticleDislikes>()
				.HasOne(uad => uad.Article)
				.WithMany(a => a.UsersDisliked)
				.HasForeignKey(uad => uad.ArticleId);

			modelBuilder.Entity<UserCommentLikes>()
				.HasKey(ucl => new { ucl.UserId, ucl.CommentId });
			modelBuilder.Entity<UserCommentLikes>()
				.HasOne(ucl => ucl.User)
				.WithMany(u => u.LikedComments)
				.HasForeignKey(ual => ual.UserId);
			modelBuilder.Entity<UserCommentLikes>()
				.HasOne(ucl => ucl.Comment)
				.WithMany(a => a.UsersLiked)
				.HasForeignKey(ucl => ucl.CommentId);

			modelBuilder.Entity<UserCommentDislikes>()
				.HasKey(ucd => new { ucd.UserId, ucd.CommentId });
			modelBuilder.Entity<UserCommentDislikes>()
				.HasOne(ucd => ucd.User)
				.WithMany(u => u.DislikedComments)
				.HasForeignKey(ucd => ucd.UserId);
			modelBuilder.Entity<UserCommentDislikes>()
				.HasOne(ucd => ucd.Comment)
				.WithMany(a => a.UsersDisliked)
				.HasForeignKey(ucd => ucd.CommentId);
		}
	}
}
