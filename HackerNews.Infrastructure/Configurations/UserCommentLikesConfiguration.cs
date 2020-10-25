using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	public class UserCommentLikesConfiguration : IEntityTypeConfiguration<UserCommentLikes>
	{
		public void Configure(EntityTypeBuilder<UserCommentLikes> builder)
		{
			builder.HasKey(ucl => new { ucl.UserId, ucl.CommentId });
			builder
				.HasOne(ucl => ucl.User)
				.WithMany(u => u.LikedComments)
				.HasForeignKey(ual => ual.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder
				.HasOne(ucl => ucl.Comment)
				.WithMany(a => a.UsersLiked)
				.HasForeignKey(ucl => ucl.CommentId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
