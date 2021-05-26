using HackerNews.CLI.ApplicationRequests;
using HackerNews.CLI.ApplicationRequests.GetEntitiesRequests;
using HackerNews.CLI.ApplicationRequests.GetEntityRequests;
using HackerNews.CLI.ApplicationRequests.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.CLI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCli(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(typeof(IGetEntityRequestHandler<,>), typeof(GetEntityRequestHandler<,>));
			services.AddSingleton(typeof(IGetEntitiesRequestHandler<,>), typeof(GetEntitiesRequestHandler<,>));
			services.AddSingleton(typeof(IPostEntityRequestHandler<,>), typeof(PostEntityRequestHandler<,>));
			services.AddSingleton<IRegisterRequestHandler, RegisterRequestHandler>();

			return services;
		}
	}
}
