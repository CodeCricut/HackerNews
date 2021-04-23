using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow.Profile;
using System;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;

		public ICommand CloseCommand { get; }

		public ICommand LogoutCommand { get; }

		public PrivateUserViewModel PrivateUserViewModel { get; }

		public MainWindowFullscreenViewModel FullscreenVM { get; }
		public MainWindowEntityViewModel EntityVM { get; }
		public EntityCreationViewModel EntityCreationViewModel { get; }
		// public EntityHomeViewModel EntityHomeViewModel { get; }

		public MainWindowViewModel(IEventAggregator ea,
			PrivateUserViewModel userVM,
			MainWindowEntityViewModel entityVm,
			MainWindowFullscreenViewModel fullscreenVm,
			EntityCreationViewModel entityCreationVm,
			EntityHomeViewModel entityHomeVm)
		{
			_ea = ea;

			PrivateUserViewModel = userVM;

			EntityVM = entityVm;
			FullscreenVM = fullscreenVm;
			EntityCreationViewModel = entityCreationVm;
			// EntityHomeViewModel = entityHomeVm;

			LogoutCommand = new DelegateCommand(SendLogoutRequest);
			CloseCommand = new DelegateCommand(SendCloseWindowRequest);
		}

		private void SendLogoutRequest(object parameter = null) => _ea.SendMessage(new LogoutRequestedMessage());

		private void SendCloseWindowRequest(object _ = null) => _ea.SendMessage(new CloseMainWindowMessage());
	}
}
