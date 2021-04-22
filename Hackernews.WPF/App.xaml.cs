using Hackernews.WPF.Configuration;
using Hackernews.WPF.Helpers;
using HackerNews.WPF.Core;
using HackerNews.WPF.MessageBus.Application;
using HackerNews.WPF.MessageBus.Core;
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

		public static Skin Skin { get; set; } = Skin.Dark;

		public void ChangeSkin(Skin skin)
		{
			Skin = skin;
			foreach (ResourceDictionary dict in Resources.MergedDictionaries)
			{

				if (dict is SkinResourceDictionary skinDict)
					skinDict.UpdateSource();
				else
					dict.Source = dict.Source;
			}
		}

		public App()
		{
			ServiceCollection services = new ServiceCollection();
			ConfigureServices(services);
			_serviceProvider = services.BuildServiceProvider();

			RegisterChangeSkinHandler();
		}

		private void RegisterChangeSkinHandler()
		{
			var ea = _serviceProvider.GetRequiredService<IEventAggregator>();
			ea.RegisterHandler<ChangeSkinMessage>(msg => ChangeSkin(msg.NewSkin));
		}

		private void ConfigureServices(ServiceCollection services)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

			services.AddWPF(config);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
			loginWindow.Show();
		}
	}
}
