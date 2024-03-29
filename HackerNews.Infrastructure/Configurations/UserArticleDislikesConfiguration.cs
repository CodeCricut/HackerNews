﻿using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	class UserArticleDislikesConfiguration : IEntityTypeConfiguration<UserArticleDislikes>
	{
		public void Configure(EntityTypeBuilder<UserArticleDislikes> builder)
		{
			builder.HasKey(uad => new { uad.UserId, uad.ArticleId });
			builder.HasOne(uad => uad.User)
				.WithMany(u => u.DislikedArticles)
				.HasForeignKey(uad => uad.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(uad => uad.Article)
				.WithMany(a => a.UsersDisliked)
				.HasForeignKey(uad => uad.ArticleId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
