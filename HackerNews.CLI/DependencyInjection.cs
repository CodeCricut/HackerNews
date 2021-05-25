using HackerNews.CLI.Loggers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.CLI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCli(this IServiceCollection services, IConfiguration configuration)
		{

			return services;
		}
	}
}
