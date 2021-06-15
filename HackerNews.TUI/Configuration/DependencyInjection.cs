using ConsoleFramework;
using ConsoleFramework.Controls;
using HackerNews.TUI.Services;
using HackerNews.WPF.Core.Services;
using HackerNews.WPF.Core.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scrutor;
using System;

namespace HackerNews.TUI.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddTUI(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<WindowsHostOptions>(configuration.GetSection(key: WindowsHostOptions.WindowsHost));
			services.AddLogging();

			services.AddSingleton<IViewManager, ViewManager>();
			services.AddSingleton<IViewFinder, ViewFinder>();

			// Register all vms
			services.AddViewModels();

			services.AddWindowsHost();

			return services;
		}

		private static void AddViewModels(this IServiceCollection services)
		{
			services.Scan(scan =>
							scan.FromCallingAssembly()
								.AddClasses(c => c.AssignableTo<BaseViewModel>()) // 1. Find the concrete vms
								.UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
								  .AsSelf() // 2. Specify which services they are registered as
								  .WithTransientLifetime());  // 3. Set the lifetime for the services
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
