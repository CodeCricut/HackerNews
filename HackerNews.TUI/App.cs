using ConsoleFramework;
using ConsoleFramework.Controls;
using HackerNews.TUI.Configuration;
using HackerNews.WPF.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			var serviceCollection = new ServiceCollection();
			var config = AppConfiguration.GetServiceConfiguration();

			serviceCollection.AddTUI(config);

			Services = serviceCollection.BuildServiceProvider();
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
