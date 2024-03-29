﻿using HackerNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	class ArticleConfiguration : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder.Property(a => a.Text).IsRequired();
			builder.Property(a => a.Title).IsRequired();
			builder.Property(a => a.UserId).IsRequired();
			builder.HasOne(a => a.User).WithMany(u => u.Articles);
			builder.Property(a => a.BoardId).IsRequired();
			builder.HasOne(a => a.Board).WithMany(b => b.Articles);
		}
	}
}
