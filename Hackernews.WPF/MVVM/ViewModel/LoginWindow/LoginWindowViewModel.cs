using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.LoginWindow;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow;
using System;
using System.Windows;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class LoginWindowViewModel : BaseViewModel
	{
		private readonly Window _thisWindow;
		private readonly Window _mainWindow;
		private readonly IEventAggregator _ea;
		private bool _loading;
		public bool Loading
		{
			get { return _loading; }
			set { _loading = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(NotLoading)); }
		}
		public bool NotLoading { get => !Loading; }

		private bool _invalidCreds;
		public bool InvalidUserInput
		{
			get { return _invalidCreds; }
			set { _invalidCreds = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ValidCreds)); }
		}
		public bool ValidCreds { get => !InvalidUserInput; }

		public ICommand CloseCommand { get; }

		#region View switcher
		private object _selectedViewModel;
		public object SelectedViewModel
		{
			get => _selectedViewModel; set
			{
				_selectedViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(LoginModelSelected));
				RaisePropertyChanged(nameof(RegisterModelSelected));
			}
		}

		public bool LoginModelSelected => SelectedViewModel == LoginModelViewModel;
		public bool RegisterModelSelected => SelectedViewModel == RegisterViewModel;

		public ICommand SelectLoginModelCommand { get; }
		public ICommand SelectRegisterModelCommand { get; }

		private void SelectLoginModel(object parameter = null) => SelectedViewModel = LoginModelViewModel;
		private void SelectRegisterModel(object parameter = null) => SelectedViewModel = RegisterViewModel;
		#endregion

		public LoginModelViewModel LoginModelViewModel { get; }

		public RegisterViewModel RegisterViewModel { get; }

		public LoginWindowViewModel(IEventAggregator ea, ISignInManager signInManager, IApiClient apiClient, LoginModelViewModel loginModelVM, RegisterViewModel registerVM)
		{
			_ea = ea;

			LoginModelViewModel = loginModelVM;
			RegisterViewModel = registerVM;

			SelectLoginModelCommand = new DelegateCommand(SelectLoginModel);
			SelectRegisterModelCommand = new DelegateCommand(SelectRegisterModel);

			SelectedViewModel = LoginModelViewModel;

			CloseCommand = new DelegateCommand(_ => ea.SendMessage(new CloseLoginWindowMessage()));

			ea.RegisterHandler<LoginWindowLoadingChangedMessage>(LoadingChanged);
			ea.RegisterHandler<LoginWindowInvalidUserInputChanged>(InvalidUserInputChanged);
			ea.RegisterHandler<LoginWindowSwitchToMainWindowMessage>(SwitchToMainWindow);
		}

		~LoginWindowViewModel()
		{
			_ea.UnregisterHandler<LoginWindowLoadingChangedMessage>(LoadingChanged);
			_ea.UnregisterHandler<LoginWindowInvalidUserInputChanged>(InvalidUserInputChanged);
			_ea.UnregisterHandler<LoginWindowSwitchToMainWindowMessage>(SwitchToMainWindow);
		}

		private void LoadingChanged(LoginWindowLoadingChangedMessage msg) => Loading = msg.IsLoading;

		private void InvalidUserInputChanged(LoginWindowInvalidUserInputChanged msg) => InvalidUserInput = msg.InvalidUserInput;

		private void SwitchToMainWindow(LoginWindowSwitchToMainWindowMessage msg)
		{
			_ea.SendMessage(new OpenMainWindowMessage());
			_ea.SendMessage(new CloseLoginWindowMessage());
		}
	}
}
