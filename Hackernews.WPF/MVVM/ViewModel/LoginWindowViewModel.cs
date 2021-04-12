using Hackernews.WPF.Helpers;
using Hackernews.WPF.Services;
using HackerNews.Domain.Common.Models.Users;
using System.Security;
using System.Threading.Tasks;
using System.Windows;

namespace Hackernews.WPF.ViewModels
{
	class LoginWindowViewModel : BaseViewModel
	{
		private readonly ISignInManager _signInManager;
		private readonly Window _thisWindow;
		private readonly Window _mainWindow;

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

		public AsyncDelegateCommand LoginCommand { get; set; }
		public LoginWindowViewModel(ISignInManager signInManager, Window thisWindow, Window mainWindow)
		{
			LoginCommand = new AsyncDelegateCommand(LoginAsync, CanLogin);
			_signInManager = signInManager;
			_thisWindow = thisWindow;
			_mainWindow = mainWindow;
		}

		private async Task LoginAsync()
		{
			// lol wut is security?
			string password = new System.Net.NetworkCredential(string.Empty, _password).Password;
			var loginModel = new LoginModel() { UserName = _username, Password = password };

			await _signInManager.SignInAsync(loginModel);

			_mainWindow.Show();
			_thisWindow.Close();
		}

		public bool CanLogin() => !(string.IsNullOrEmpty(Username) || Password?.Length <= 0);
	}
}
