using HackerNews.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infrastructure.Configurations
{
	public class BoardUserSubscriberConfiguration : IEntityTypeConfiguration<BoardUserSubscriber>
	{
		public void Configure(EntityTypeBuilder<BoardUserSubscriber> builder)
		{
			builder.HasKey(bu => new { bu.UserId, bu.BoardId });
			builder
				.HasOne(bu => bu.User)
				.WithMany(u => u.BoardsSubscribed)
				.HasForeignKey(bu => bu.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder
				.HasOne(bu => bu.Board)
				.WithMany(b => b.Subscribers)
				.HasForeignKey(bu => bu.BoardId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
