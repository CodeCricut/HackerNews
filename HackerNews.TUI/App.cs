using ConsoleFramework;
using ConsoleFramework.Controls;
using HackerNews.ApiConsumer;
using HackerNews.MessageBus;
using HackerNews.TUI.Configuration;
using HackerNews.TUI.ViewModels;
using HackerNews.WPF.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HackerNews.TUI
{
	public sealed class App
	{
		private static volatile App _instance;
		private static readonly object _syncRoot = new();
		public static App Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_syncRoot)
					{
						if (_instance == null)
						{
							_instance = new App();
						}
					}
				}
				return _instance;
			}
		}

		public IServiceProvider Services { get; init; }

		public App()
		{
			var services = new ServiceCollection();
			var config = AppConfiguration.GetServiceConfiguration();

			services.AddTUI(config);
			services.AddMessageBus(config);
			services.AddApiConsumer(config);

			Services = services.BuildServiceProvider();
		}

		public void Run()
		{
			var viewManager = Services.GetRequiredService<IViewManager>();
			var windowsHost = Services.GetRequiredService<WindowsHost>();
			var loginVm = Services.GetRequiredService<LoginWindowViewModel>();
			
			viewManager.Show(loginVm);

			ConsoleApplication.Instance.Run(windowsHost);
		}

	}
}
