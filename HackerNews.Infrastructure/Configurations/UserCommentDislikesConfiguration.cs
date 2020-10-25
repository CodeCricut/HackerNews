using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infrastructure.Configurations
{
	public class UserCommentDislikesConfiguration : IEntityTypeConfiguration<UserCommentDislikes>
	{
		public void Configure(EntityTypeBuilder<UserCommentDislikes> builder)
		{
			builder.HasKey(ucd => new { ucd.UserId, ucd.CommentId });
			builder
				.HasOne(ucd => ucd.User)
				.WithMany(u => u.DislikedComments)
				.HasForeignKey(ucd => ucd.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder
				.HasOne(ucd => ucd.Comment)
				.WithMany(a => a.UsersDisliked)
				.HasForeignKey(ucd => ucd.CommentId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
