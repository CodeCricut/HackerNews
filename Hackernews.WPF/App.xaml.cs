using Hackernews.WPF.Configuration;
using Hackernews.WPF.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly ServiceProvider _serviceProvider;
		private readonly IConfigurationRoot _configuration;

		public static Skin Skin { get; set; } = Skin.Light;

		public App()
		{
			_configuration = CreateConfiguration();

			ServiceCollection services = new ServiceCollection();
			ConfigureServices(services);
			_serviceProvider = services.BuildServiceProvider();

			Application.Current.Properties.Add("services", _serviceProvider);
		}

		private static IConfigurationRoot CreateConfiguration()
		{
			var configBuilder = new ConfigurationBuilder();
			configBuilder.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();
			return configBuilder.Build();
		}

		private void ConfigureServices(ServiceCollection services)
		{
			//services.AddDomain(_configuration);
			//services.AddInfrastructure(_configuration);
			//services.AddApplication();
			services.AddWPF(_configuration);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
			loginWindow.Show();

			//var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
			//mainWindow.Show();
		}
	}
}
