using Hackernews.WPF.Helpers;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	class LoginViewModel : BaseViewModel
	{
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
		private readonly LoginWindowViewModel _loginWindowVM;
		private readonly ISignInManager _signInManager;

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

		public LoginViewModel(LoginWindowViewModel loginWindowVM, ISignInManager signInManager)
		{
			_loginWindowVM = loginWindowVM;
			_signInManager = signInManager;

			LoginCommand = new AsyncDelegateCommand(LoginAsync, CanLogin);
		}

		public AsyncDelegateCommand LoginCommand { get; }

		public bool CanLogin(object parameter = null) => !(string.IsNullOrEmpty(Username) || Password?.Length <= 0);

		private async Task LoginAsync(object parameter = null)
		{
			_loginWindowVM.Loading = true;
			_loginWindowVM.InvalidCreds = false;

			// lol wut is security?
			string password = new System.Net.NetworkCredential(string.Empty, _password).Password;
			var loginModel = new LoginModel() { UserName = _username, Password = password };

			try
			{
				await _signInManager.SignInAsync(loginModel);

				_loginWindowVM.SwitchToMainWindowAction();
			}
			catch (System.Exception e)
			{
				Username = "";
				Password.Clear();

				_loginWindowVM.InvalidCreds = true;
			}
			finally
			{
				_loginWindowVM.Loading = false;
			}

		}
	}
}
