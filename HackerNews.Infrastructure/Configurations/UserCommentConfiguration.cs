﻿using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	class UserCommentConfiguration : IEntityTypeConfiguration<UserComment>
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
