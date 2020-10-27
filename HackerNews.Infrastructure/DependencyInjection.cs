using HackerNews.Domain.Interfaces;
using HackerNews.EF;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HackerNews.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
				// In order to run tests in parallel without issues, DB name must be unique.
				var inMemoryDbName = $"HackerNewsDb{Guid.NewGuid()}";

				services.AddDbContext<DbContext, HackerNewsContext>(options =>
					options.UseInMemoryDatabase(inMemoryDbName)); ;
			}
			else
			{
				services.AddDbContext<DbContext, HackerNewsContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("HackerNews")
				));
			}

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddHttpContextAccessor();

			// I choose not to register the repositories manually, because they should really only be used in conjunction with IUnitOfWork

			return services;
		}
	}
}
