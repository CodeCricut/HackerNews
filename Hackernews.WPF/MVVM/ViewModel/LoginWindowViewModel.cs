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
	class LoginWindowViewModel : BaseViewModel
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
		public bool InvalidCreds
		{
			get { return _invalidCreds; }
			set { _invalidCreds = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ValidCreds)); }
		}
		public bool ValidCreds { get => !InvalidCreds; }

		public Action CloseAction { get; set; }
		public ICommand CloseCommand { get; }

		public Action SwitchToMainWindowAction { get; }

		public LoginViewModel LoginViewModel { get; }

		public LoginWindowViewModel(ISignInManager signInManager, Window thisWindow, Window mainWindow)
		{
			_thisWindow = thisWindow;
			_mainWindow = mainWindow;

			LoginViewModel = new LoginViewModel(this, signInManager);

			SwitchToMainWindowAction = () =>
			{
				_mainWindow.Show();
				_thisWindow.Close();
			};

			CloseCommand = new DelegateCommand(_ => CloseAction?.Invoke());
		}
	}
}
