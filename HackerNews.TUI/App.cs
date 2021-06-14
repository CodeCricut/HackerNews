using ConsoleFramework;
using ConsoleFramework.Controls;
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

		public WindowsHost WindowsHost { get; init; } // TODO: register as singleton

		public App()
		{
			var serviceCollection = new ServiceCollection();
			var config = AppConfiguration.GetServiceConfiguration();

			serviceCollection.AddTUI(config);

			Services = serviceCollection.BuildServiceProvider();

			WindowsHost = (WindowsHost)ConsoleApplication.LoadFromXaml("HackerNews.TUI.windows-host.xml", null); // TODO: load with config file and/or reflection.
		}


		public void Run()
		{
			var viewManager = Services.GetRequiredService<IViewManager>();

			var vm = new LoginWindowViewModel();
			viewManager.Show(vm);

			ConsoleApplication.Instance.Run(WindowsHost);
		}

	}
}
