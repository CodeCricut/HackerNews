using Hackernews.WPF.Configuration;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.Services;
using HackerNews.ApiConsumer;
using HackerNews.WPF.Core;
using HackerNews.WPF.MessageBus;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using Microsoft.Extensions.DependencyInjection;
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
			services.AddMessageBus(config);
			services.AddApiConsumer(config);

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
