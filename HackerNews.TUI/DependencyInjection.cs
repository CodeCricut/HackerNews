using HackerNews.TUI.Services;
using HackerNews.WPF.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.TUI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddTUI(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddLogging();

			services.AddSingleton<IViewManager, ViewManager>();

			return services;
		}
	}
}
