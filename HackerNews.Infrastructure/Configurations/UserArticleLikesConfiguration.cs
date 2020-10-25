using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	public class UserArticleLikesConfiguration : IEntityTypeConfiguration<UserArticleLikes>
	{
		public void Configure(EntityTypeBuilder<UserArticleLikes> builder)
		{
			builder.HasKey(ual => new { ual.UserId, ual.ArticleId });
			builder.HasOne(ual => ual.User)
				.WithMany(u => u.LikedArticles)
				.HasForeignKey(ual => ual.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(ual => ual.Article)
				.WithMany(a => a.UsersLiked)
				.HasForeignKey(ual => ual.ArticleId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
