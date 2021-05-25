using HackerNews.CLI.ApplicationRequests;
using HackerNews.CLI.ApplicationRequests.GetEntitiesRequests;
using HackerNews.CLI.ApplicationRequests.GetEntityRequests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.CLI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCli(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(typeof(IGetEntityRequestAggregator<>), typeof(GetEntityRequestAggregator<>));
			services.AddSingleton(typeof(IGetEntitiesRequestAggregator<>), typeof(GetEntitiesRequestAggregator<>));
			services.AddSingleton(typeof(IPostEntityRequestAggregator<,>), typeof(PostEntityRequestAggregator<,>));

			return services;
		}
	}
}
