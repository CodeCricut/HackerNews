using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infrastructure.Configurations
{
	public class UserCommentConfiguration : IEntityTypeConfiguration<UserComment>
	{
		public void Configure(EntityTypeBuilder<UserComment> builder)
		{
			builder.HasKey(uc => new { uc.UserId, uc.CommentId });
			builder.HasOne(uc => uc.User)
				.WithMany(u => u.SavedComments)
				.HasForeignKey(uc => uc.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(uc => uc.Comment)
				.WithMany(a => a.UsersSaved)
				.HasForeignKey(uc => uc.CommentId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
