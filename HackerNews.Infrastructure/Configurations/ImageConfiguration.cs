using HackerNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	public class ImageConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			builder
				.HasOne(i => i.Article).WithOne(a => a.AssociatedImage)
				.HasForeignKey<Image>(i => i.ArticleId);

			builder
				.HasOne(i => i.Board).WithOne(b => b.BoardImage)
				.HasForeignKey<Image>(i => i.BoardId);

			builder
				.HasOne(i => i.User).WithOne(u => u.ProfileImage)
				.HasForeignKey<Image>(i => i.UserId);
		}
	}
}
