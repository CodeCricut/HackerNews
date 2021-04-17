using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class LoginWindowViewModel : BaseViewModel
	{
		private readonly Window _thisWindow;
		private readonly Window _mainWindow;

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

		public Action CloseAction { get; set; }
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

		public bool LoginModelSelected => SelectedViewModel == LoginViewModel;
		public bool RegisterModelSelected => SelectedViewModel == RegisterViewModel;

		public ICommand SelectLoginModelCommand { get; }
		public ICommand SelectRegisterModelCommand { get; }

		private void SelectLoginModel(object parameter = null) => SelectedViewModel = LoginViewModel;
		private void SelectRegisterModel(object parameter = null) => SelectedViewModel = RegisterViewModel;
		#endregion

		public LoginViewModel LoginViewModel { get; }

		public RegisterViewModel RegisterViewModel { get; }

		public LoginWindowViewModel(ISignInManager signInManager, IApiClient apiClient, Window thisWindow, Window mainWindow)
		{
			_thisWindow = thisWindow;
			_mainWindow = mainWindow;

			LoginViewModel = new LoginViewModel(this, signInManager);
			RegisterViewModel = new RegisterViewModel(this, signInManager, apiClient);

			SelectLoginModelCommand = new DelegateCommand(SelectLoginModel);
			SelectRegisterModelCommand = new DelegateCommand(SelectRegisterModel);

			SelectedViewModel = LoginViewModel;

			CloseCommand = new DelegateCommand(_ => CloseAction?.Invoke());
		}

		public void SwitchToMainWindow()
		{
			_mainWindow.Show();
			_thisWindow.Close();
		}
	}
}
