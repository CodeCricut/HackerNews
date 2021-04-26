using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel;
using HackerNews.ApiConsumer.Core;
using HackerNews.WPF.Core;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using System;
using System.Threading.Tasks;

namespace Hackernews.WPF.Services
{
	// TODO: extract view instantiation to separate handler
	public class AppMessageHandler
	{
		private readonly IEventAggregator _ea;
		private readonly ISignInManager _signInManager;
		private readonly IMainWindowVmFactory _mainWindowVmFactory;
		private readonly ILoginWindowVmFactory _loginWindowVmFactory;
		private readonly EntityCreationViewModel _entityCreationVm;

		public AppMessageHandler(IEventAggregator ea,
			ISignInManager signInManager,
			IMainWindowVmFactory mainWindowVmFactory,
			ILoginWindowVmFactory loginWindowVmFactory,
			EntityCreationViewModel entityCreationVm
			)
		{
			_ea = ea;
			_signInManager = signInManager;
			_mainWindowVmFactory = mainWindowVmFactory;
			_loginWindowVmFactory = loginWindowVmFactory;
			_entityCreationVm = entityCreationVm;
			ea.RegisterHandler<ChangeSkinMessage>(msg => ChangeSkin(msg.NewSkin));
			
			ea.RegisterHandler<LogoutRequestedMessage>(async msg => await LogoutAsync());
			ea.RegisterHandler<CloseApplicationMessage>(msg => CloseApplication());

			ea.RegisterHandler<OpenMainWindowMessage>(msg => OpenMainWindow());
			ea.RegisterHandler<OpenLoginWindowMessage>(msg => OpenLoginWindow());
			//ea.RegisterHandler<OpenEntityCreationWindowMessage>(msg => OpenEntityCreationWindow());

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
		}

		private void OpenLoginWindow()
		{
			var loginWindowVm = _loginWindowVmFactory.Create();
			loginWindowVm.ShowWindow();
		}


		//private void OpenEntityCreationWindow()
		//{
		//	_entityCreationVm.OpenWindow();
		//}


		private void LoginWindowSwitchToMainWindow(LoginWindowSwitchToMainWindowMessage msg)
		{
			_ea.SendMessage(new OpenMainWindowMessage());
			_ea.SendMessage(new CloseLoginWindowMessage());
		}

		private void MainWindowSwitchToLoginWindow(MainWindowSwitchToLoginWindowMessage msg)
		{
			_ea.SendMessage(new OpenLoginWindowMessage());
			_ea.SendMessage(new CloseMainWindowMessage());
		}

		private async Task LogoutAsync() => await _signInManager.SignOutAsync();

		private void CloseApplication() => 	App.Current.Shutdown();
	}
}
