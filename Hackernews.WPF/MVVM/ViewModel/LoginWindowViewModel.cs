using Hackernews.WPF.Helpers;
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
		private readonly ISignInManager _signInManager;
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



		private string _username;
		public string Username
		{
			get => _username;
			set
			{
				if (_username != value)
				{
					_username = value;
					RaisePropertyChanged();
					LoginCommand.RaiseCanExecuteChanged();
				}
			}
		}

		private SecureString _password;
		public SecureString Password
		{
			private get => _password;
			set
			{
				if (_password != value)
				{
					_password = value;
					RaisePropertyChanged();
					LoginCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public Action CloseAction { get; set; }
		public ICommand CloseCommand { get; }

		public AsyncDelegateCommand LoginCommand { get; }

		public LoginWindowViewModel(ISignInManager signInManager, Window thisWindow, Window mainWindow)
		{
			LoginCommand = new AsyncDelegateCommand(LoginAsync, CanLogin);
			CloseCommand = new DelegateCommand(_ => CloseAction?.Invoke());

			_signInManager = signInManager;
			_thisWindow = thisWindow;
			_mainWindow = mainWindow;
		}

		public bool CanLogin(object parameter = null) => !(string.IsNullOrEmpty(Username) || Password?.Length <= 0);

		private async Task LoginAsync(object parameter = null)
		{
			Loading = true;
			InvalidCreds = false;

			// lol wut is security?
			string password = new System.Net.NetworkCredential(string.Empty, _password).Password;
			var loginModel = new LoginModel() { UserName = _username, Password = password };

			try
			{
				await _signInManager.SignInAsync(loginModel);

				_mainWindow.Show();
				_thisWindow.Close();
			}
			catch (System.Exception e)
			{
				Username = "";
				Password.Clear();

				InvalidCreds = true;
			}
			finally
			{
				Loading = false;
			}

		}
	}
}
