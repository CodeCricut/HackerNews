using ConsoleFramework;
using ConsoleFramework.Controls;
using HackerNews.TUI.Configuration;
using HackerNews.TUI.Services;
using HackerNews.WPF.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace HackerNews.TUI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddTUI(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<WindowsHostOptions>(configuration.GetSection(key: WindowsHostOptions.WindowsHost));
			services.AddLogging();

			services.AddSingleton<IViewManager, ViewManager>();
			
			services.AddWindowsHost();

			return services;
		}

		private static void AddWindowsHost(this IServiceCollection services)
		{
			var currentSP = services.GetUnfinishedServiceProvider();
			WindowsHost windowsHost = GetWindowsHost(currentSP);
			services.AddSingleton(windowsHost);
		}

		private static WindowsHost GetWindowsHost(IServiceProvider serviceProvider)
		{
			IOptions<WindowsHostOptions> opts = serviceProvider.GetRequiredService<IOptions<WindowsHostOptions>>();
			WindowsHostOptions windowsHostOpts = opts.Value;

			string currentAssemblyName = windowsHostOpts.GetType().Assembly.GetName().Name;

			return (WindowsHost)ConsoleApplication.LoadFromXaml($"{currentAssemblyName}.{windowsHostOpts.FileName}", null);
		}

		private static IServiceProvider GetUnfinishedServiceProvider(this IServiceCollection services)
		{
			return services.BuildServiceProvider();
		}
	}
}
