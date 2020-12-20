using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HackerNews.EF
{
	public class HackerNewsContext : IdentityDbContext<User, IdentityRole<int>, int>
	{
		public DbSet<Article> Articles { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Board> Boards { get; set; }
		public DbSet<Image> Images { get; set; }

		// we will inject the configuration options in through the api/startup class
		public HackerNewsContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
