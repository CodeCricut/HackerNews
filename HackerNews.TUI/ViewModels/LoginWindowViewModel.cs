using ConsoleFramework.Events;
using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using System;
using System.Threading.Tasks;

namespace HackerNews.TUI.ViewModels
{
	public class LoginWindowViewModel : BaseViewModel
	{
		private string _loadingMessage;
		public string LoadingMessage
		{
			get { return _loadingMessage; }
			set { Set(ref _loadingMessage, value); }
		}

		private string _warningMessage;
		public string WarningMessage
		{
			get { return _warningMessage; }
			set { Set(ref _warningMessage, value); }
		}


		private string _username;
		public string Username
		{
			get { return _username; }
			set
			{
				Set(ref _username, value);
				_loginCommand.RaiseCanExecuteChanged();
			}
		}

		private string _password;

		public string Password
		{
			get { return _password; }
			set
			{
				Set(ref _password, value);
				_loginCommand.RaiseCanExecuteChanged();
			}
		}

		private AsyncDelegateCommand _loginCommand;
		public ICommand LoginCommand { get => _loginCommand; }

		private readonly ISignInManager _signInManager;

		public LoginWindowViewModel(ISignInManager signInManager)
		{
			_loginCommand = new AsyncDelegateCommand(LoginAsync, CanLogin);
			_signInManager = signInManager;

			NotLoading();
		}

		public bool CanLogin(object _ = null) => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));

		private async Task LoginAsync(object _ = null)
		{
			Loading();
			NoWarning();

			try
			{
				var loginModel = new LoginModel() { UserName = Username, Password = Password };
				await _signInManager.SignInAsync(loginModel);
			}
			catch (Exception)
			{
				Warning();
			}
			finally
			{
				NotLoading();
			}
		}

		private void NotLoading()
		{
			LoadingMessage = string.Empty;
		}

		private void Loading()
		{
			LoadingMessage = "Loading...";
		}

		private void Warning()
		{
			WarningMessage = "Invalid credentials.";
		}

		private void NoWarning()
		{
			WarningMessage = string.Empty;
		}
	}
}
