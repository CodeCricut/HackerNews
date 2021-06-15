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
		}

		public bool CanLogin(object _ = null) => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));

		private async Task LoginAsync(object _ = null)
		{
			try
			{
				var loginModel = new LoginModel() { UserName = Username, Password = Password };
				await _signInManager.SignInAsync(loginModel);
			}
			catch (Exception)
			{
			}
		}
	}
}
