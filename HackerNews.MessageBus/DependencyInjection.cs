using HackerNews.MessageBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.MessageBus
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IEventAggregator, EventAggregator>();
			return services;
		}
	}
}
