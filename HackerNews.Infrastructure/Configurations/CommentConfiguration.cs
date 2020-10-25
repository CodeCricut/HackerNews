using HackerNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infrastructure.Configurations
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.Property(c => c.Text).IsRequired();
			builder.Property(c => c.UserId).IsRequired();
			builder.HasOne(c => c.User).WithMany(u => u.Comments);
			builder.Property(c => c.BoardId).IsRequired();
			builder.HasOne(c => c.Board).WithMany(b => b.Comments);
		}
	}
}
