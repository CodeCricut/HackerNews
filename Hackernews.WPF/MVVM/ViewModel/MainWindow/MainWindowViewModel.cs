using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public Action CloseAction { get; set; }
		public ICommand CloseCommand { get; }

		public Action LogoutAction { get; set; }
		public ICommand LogoutCommand { get; }

		public PrivateUserViewModel PrivateUserViewModel { get; }

		public MainWindowFullscreenViewModel FullscreenVM { get; }
		public MainWindowEntityViewModel EntityVM { get; }
		public EntityCreationViewModel EntityCreationViewModel { get; }

		public MainWindowViewModel(IApiClient apiClient, PrivateUserViewModel userVM)
		{
			CloseCommand = new DelegateCommand(_ => CloseAction?.Invoke());
			LogoutCommand = new DelegateCommand(_ => LogoutAction?.Invoke());

			PrivateUserViewModel = new PrivateUserViewModel(apiClient);

			EntityVM = new MainWindowEntityViewModel(this, userVM, apiClient);
			FullscreenVM = new MainWindowFullscreenViewModel(this);
			EntityCreationViewModel = new EntityCreationViewModel(apiClient);
		}
	}
}
