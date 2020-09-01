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



		// this is what we would do if we weren't injecting our context options
		/*
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Database = HackerNews; Trusted_Connection = True; ");
		}
		*/
	}
}
