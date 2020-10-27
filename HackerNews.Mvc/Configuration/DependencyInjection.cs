using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddMvcProject(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllersWithViews();

			return services;
		}
	}
}
