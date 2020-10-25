using HackerNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackerNews.Infrastructure.Configurations
{
	public class BoardConfiguration : IEntityTypeConfiguration<Board>
	{
		public void Configure(EntityTypeBuilder<Board> builder)
		{
		}
	}
}
