using ConsoleFramework;
using ConsoleFramework.Controls;
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
			WindowsHost windowsHost = GetWindowsHost();

			services.AddSingleton(windowsHost);

			return services;
		}

		private static WindowsHost GetWindowsHost()
		{
			return (WindowsHost)ConsoleApplication.LoadFromXaml("HackerNews.TUI.windows-host.xml", null);
			// TODO: load with config file and/or reflection.
		}
	}
}
