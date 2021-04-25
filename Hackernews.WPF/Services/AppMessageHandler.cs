using Hackernews.WPF.Messages.Application;
using Hackernews.WPF.Messages.ViewModel.LoginWindow;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.Core;
using HackerNews.WPF.MessageBus.Application;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow.Profile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hackernews.WPF.Services
{
	public class AppMessageHandler
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly ISignInManager _signInManager;
		private readonly PrivateUserViewModel _userVm;
		private readonly MainWindowEntityViewModel _mainWindowEntityVm;
		private readonly MainWindowFullscreenViewModel _mainWindowFullscreenVm;
		private readonly EntityCreationViewModel _entityCreationVm;
		private readonly LoginModelViewModel _loginModelVm;
		private readonly RegisterViewModel _registerVm;
		private MainWindowViewModel _mainWindowVm;
		private LoginWindowViewModel _loginWindowVm;

		public AppMessageHandler(IEventAggregator ea,
			IViewManager viewManager,
			ISignInManager signInManager,
			PrivateUserViewModel userVm,
			MainWindowEntityViewModel mainWindowEntityVm,
			MainWindowFullscreenViewModel mainWindowFullscreenVm,
			EntityCreationViewModel entityCreationVm,
			LoginModelViewModel loginModelVm,
			RegisterViewModel registerVm
			)
		{
			_ea = ea;
			_viewManager = viewManager;
			_signInManager = signInManager;
			_userVm = userVm;
			_mainWindowEntityVm = mainWindowEntityVm;
			_mainWindowFullscreenVm = mainWindowFullscreenVm;
			_entityCreationVm = entityCreationVm;
			_loginModelVm = loginModelVm;
			_registerVm = registerVm;

			ea.RegisterHandler<ChangeSkinMessage>(msg => ChangeSkin(msg.NewSkin));

			ea.RegisterHandler<OpenMainWindowMessage>(msg => OpenMainWindow());
			ea.RegisterHandler<OpenLoginWindowMessage>(msg => OpenLoginWindow());

			ea.RegisterHandler<LogoutRequestedMessage>(async msg => await LogoutAsync());

			ea.RegisterHandler<CloseApplicationMessage>(msg => CloseApplication());

			ea.RegisterHandler<LoginWindowSwitchToMainWindowMessage>(LoginWindowSwitchToMainWindow);
			ea.RegisterHandler<MainWindowSwitchToLoginWindowMessage>(MainWindowSwitchToLoginWindow);
		}

		private void ChangeSkin(Skin newSkin)
		{
			(App.Current as App).ChangeSkin(newSkin);
		}

		private void OpenMainWindow()
		{
			_mainWindowVm = new MainWindowViewModel(_ea, _viewManager, _userVm, _mainWindowEntityVm, _mainWindowFullscreenVm, _entityCreationVm);
			_mainWindowVm.OpenWindow();
		}

		private void OpenLoginWindow()
		{
			_loginWindowVm = new LoginWindowViewModel(_ea, _loginModelVm, _registerVm, _viewManager);
			_loginWindowVm.ShowWindow();
		}

		private void LoginWindowSwitchToMainWindow(LoginWindowSwitchToMainWindowMessage msg)
		{
			OpenMainWindow();
			_loginWindowVm?.CloseWindow();
		}

		private void MainWindowSwitchToLoginWindow(MainWindowSwitchToLoginWindowMessage msg)
		{
			OpenLoginWindow();
			_mainWindowVm?.CloseWindow();
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
