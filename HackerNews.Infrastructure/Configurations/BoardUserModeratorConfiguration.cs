using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	class BoardUserModeratorConfiguration : IEntityTypeConfiguration<BoardUserModerator>
	{
		public void Configure(EntityTypeBuilder<BoardUserModerator> builder)
		{
			builder.HasKey(bu => new { bu.UserId, bu.BoardId });
			builder
				.HasOne(bu => bu.User)
				.WithMany(u => u.BoardsModerating)
				.HasForeignKey(bu => bu.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder
				.HasOne(bu => bu.Board)
				.WithMany(b => b.Moderators)
				.HasForeignKey(bu => bu.BoardId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
