using HackerNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infrastructure.Configurations
{
	public class BoardConfiguration : IEntityTypeConfiguration<Board>
	{
		public void Configure(EntityTypeBuilder<Board> builder)
		{
		}
	}
}
