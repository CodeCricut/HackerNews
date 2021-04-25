using Hackernews.WPF.Configuration;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.Messages.Application;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.Core;
using HackerNews.WPF.MessageBus.Application;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow.Profile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public ServiceProvider ServiceProvider { get; }

		public static Skin Skin { get; set; } = Skin.Dark;

		private static AppMessageHandler _appMessageHandler;

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
			var config = AppConfiguration.GetServiceConfiguration();
			services.AddWPF(config);

			ServiceProvider = services.BuildServiceProvider();

			_appMessageHandler = ServiceProvider.GetRequiredService<AppMessageHandler>();
		}


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var ea = ServiceProvider.GetRequiredService<IEventAggregator>();

			ea.SendMessage(new OpenLoginWindowMessage());
		}
	}
}
