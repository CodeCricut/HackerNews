using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.EF
{
	public class HackerNewsContext : DbContext
	{
		public DbSet<Article> Articles { get; set; }
		public DbSet<Comment> Comments { get; set; }

		// we will inject the configuration options in through the api/startup class
		public HackerNewsContext(DbContextOptions<HackerNewsContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<Article>().HasKey(a => a.Id);
			////	.HasMany(a => a.Comments)
			////	.WithOne(c => c.ParentArticle)
			////	.OnDelete(DeleteBehavior.SetNull);

			//modelBuilder.Entity<Comment>().HasKey(c => c.Id);

			//modelBuilder.Entity<Comment>()
			//	.HasOne(c => c.ParentArticle).WithMany(a => a.Comments)
			//	.HasForeignKey(childComment => childComment.ParentArticleId).IsRequired(false);

			//modelBuilder.Entity<Comment>()
			//	.HasOne(c => c.ParentComment).WithMany(pc => pc.ChildComments); 
				//.HasForeignKey(childComment => childComment.ParentCommentId).IsRequired(false);

				//.HasMany(c => c.Comments)
				//.WithOne(c => c.ParentComment)
				//.OnDelete(DeleteBehavior.SetNull)
				//.HasForeignKey(c => c.ParentCommentId)
				//.HasForeignKey(c => c.ParentArticleId);
		}


		// this is what we would do if we weren't injecting our context options
		/*
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Database = HackerNews; Trusted_Connection = True; ");
		}
		*/
	}
}
