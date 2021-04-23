using Hackernews.WPF.Configuration;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.Services;
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
		private readonly ServiceProvider _serviceProvider;
		private readonly IEventAggregator _ea;
		private readonly ISignInManager _signInManager;

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

			_ea = _serviceProvider.GetRequiredService<IEventAggregator>();
			_signInManager = _serviceProvider.GetRequiredService<ISignInManager>();

			RegisterApplicationHandlers();
		}

		private void RegisterApplicationHandlers()
		{
			_ea.RegisterHandler<ChangeSkinMessage>(msg => ChangeSkin(msg.NewSkin));
			_ea.RegisterHandler<OpenMainWindowMessage>(msg => OpenMainWindow());
			_ea.RegisterHandler<CloseApplicationMessage>(msg => Application.Current.Shutdown());
			_ea.RegisterHandler<LogoutRequestedMessage>(async msg => { await Logout(); OpenLoginWindow(); });
		}

		private void OpenMainWindow()
		{
			var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
		}

		private void OpenLoginWindow()
		{
			var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
			loginWindow.Show();
		}

		private async Task Logout()
		{
			await _signInManager.SignOutAsync();
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
