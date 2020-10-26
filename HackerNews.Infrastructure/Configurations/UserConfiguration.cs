using HackerNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	 class UserConfiguration : IEntityTypeConfiguration<User>
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
