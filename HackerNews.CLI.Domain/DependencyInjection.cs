using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCliDomain(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IVerbAccessor, VerbAccessor>();
			return services;
		}
	}
}
