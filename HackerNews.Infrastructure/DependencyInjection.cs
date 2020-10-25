using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.EF;
using HackerNews.Infrastructure.Repository.Common;
using HackerNews.Infrastructure.Repository.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
				services.AddDbContext<DbContext, HackerNewsContext>(options =>
					options.UseInMemoryDatabase("HackerNewsDb"));
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
