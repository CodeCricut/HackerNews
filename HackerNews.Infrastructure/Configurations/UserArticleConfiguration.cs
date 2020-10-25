using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	public class UserArticleConfiguration : IEntityTypeConfiguration<UserArticle>
	{
		public void Configure(EntityTypeBuilder<UserArticle> builder)
		{
			builder.HasKey(ua => new { ua.UserId, ua.ArticleId });
			builder.HasOne(ua => ua.User)
				.WithMany(u => u.SavedArticles)
				.HasForeignKey(ua => ua.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(ua => ua.Article)
				.WithMany(a => a.UsersSaved)
				.HasForeignKey(ua => ua.ArticleId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
