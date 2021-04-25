using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.Messages.Application;
using Hackernews.WPF.Messages.ViewModel.LoginWindow;
using HackerNews.WPF.Core;
using HackerNews.WPF.MessageBus.Application;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow.Profile;
using System.Threading.Tasks;

namespace Hackernews.WPF.Services
{
	public class AppMessageHandler
	{
		private readonly IEventAggregator _ea;
		private readonly ISignInManager _signInManager;
		private readonly IMainWindowVmFactory _mainWindowVmFactory;
		private readonly ILoginWindowVmFactory _loginWindowVmFactory;

		public AppMessageHandler(IEventAggregator ea,
			ISignInManager signInManager,
			IMainWindowVmFactory mainWindowVmFactory,
			ILoginWindowVmFactory loginWindowVmFactory
			)
		{
			_ea = ea;
			_signInManager = signInManager;
			_mainWindowVmFactory = mainWindowVmFactory;
			_loginWindowVmFactory = loginWindowVmFactory;

			ea.RegisterHandler<ChangeSkinMessage>(msg => ChangeSkin(msg.NewSkin));
			
			ea.RegisterHandler<LogoutRequestedMessage>(async msg => await LogoutAsync());
			ea.RegisterHandler<CloseApplicationMessage>(msg => CloseApplication());

			ea.RegisterHandler<OpenMainWindowMessage>(msg => OpenMainWindow());
			ea.RegisterHandler<OpenLoginWindowMessage>(msg => OpenLoginWindow());

			ea.RegisterHandler<LoginWindowSwitchToMainWindowMessage>(LoginWindowSwitchToMainWindow);
			ea.RegisterHandler<MainWindowSwitchToLoginWindowMessage>(MainWindowSwitchToLoginWindow);
		}

		private void ChangeSkin(Skin newSkin)
		{
			(App.Current as App).ChangeSkin(newSkin);
		}

		private void OpenMainWindow()
		{
			var mainWindowVm = _mainWindowVmFactory.Create();
			mainWindowVm.OpenWindow();
			//_mainWindowVm = new MainWindowViewModel(_ea, _viewManager, _userVm, _mainWindowEntityVm, _mainWindowFullscreenVm, _entityCreationVm);
			//_mainWindowVm.OpenWindow();
		}

		private void OpenLoginWindow()
		{
			var loginWindowVm = _loginWindowVmFactory.Create();
			loginWindowVm.ShowWindow();
			//_loginWindowVm = new LoginWindowViewModel(_ea, _loginModelVm, _registerVm, _viewManager);
			//_loginWindowVm.ShowWindow();
		}

		private void LoginWindowSwitchToMainWindow(LoginWindowSwitchToMainWindowMessage msg)
		{
			_ea.SendMessage(new OpenMainWindowMessage());
			_ea.SendMessage(new CloseLoginWindowMessage());
			//OpenMainWindow();
			//_loginWindowVm?.CloseWindow();
		}

		private void MainWindowSwitchToLoginWindow(MainWindowSwitchToLoginWindowMessage msg)
		{
			_ea.SendMessage(new OpenLoginWindowMessage());
			_ea.SendMessage(new CloseMainWindowMessage());
			//OpenLoginWindow();
			//_mainWindowVm?.CloseWindow();
		}

		private async Task LogoutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		private void CloseApplication()
		{
			App.Current.Shutdown();
		}

	}
}
