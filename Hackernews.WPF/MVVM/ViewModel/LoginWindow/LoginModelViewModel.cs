using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.MessageBus.Core;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Messages.Application;
using HackerNews.WPF.MessageBus.Messages.ViewModel.LoginWindow;
using System.Security;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class LoginModelViewModel : BaseViewModel
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

		private readonly IEventAggregator _ea;
		private readonly ISignInManager _signInManager;

		public LoginModelViewModel(IEventAggregator ea, ISignInManager signInManager)
		{
			_ea = ea;
			_signInManager = signInManager;

			LoginCommand = new AsyncDelegateCommand(LoginAsync, CanLogin);
		}

		public AsyncDelegateCommand LoginCommand { get; }

		public bool CanLogin(object parameter = null) => !(string.IsNullOrEmpty(Username) || Password?.Length <= 0);

		private async Task LoginAsync(object parameter = null)
		{
			_ea.SendMessage(new LoginWindowLoadingChangedMessage(isLoading: true));
			_ea.SendMessage(new LoginWindowInvalidUserInputChanged(invalidUserInput: false));

			// lol wut is security?
			string password = new System.Net.NetworkCredential(string.Empty, _password).Password;
			var loginModel = new LoginModel() { UserName = _username, Password = password };

			try
			{
				await _signInManager.SignInAsync(loginModel);

				_ea.SendMessage(new LoginWindowSwitchToMainWindowMessage());
			}
			catch (System.Exception)
			{
				Username = "";
				Password.Clear();

				_ea.SendMessage(new LoginWindowInvalidUserInputChanged(invalidUserInput: true));
			}
			finally
			{
				_ea.SendMessage(new LoginWindowLoadingChangedMessage(isLoading: false));
			}
		}
	}
}
