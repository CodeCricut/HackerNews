using HackerNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infrastructure.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.FirstName).IsRequired();
			builder.Property(u => u.LastName).IsRequired();
			builder.Property(u => u.Username).IsRequired();
			builder.Property(u => u.Password).IsRequired();
		}
	}
}
