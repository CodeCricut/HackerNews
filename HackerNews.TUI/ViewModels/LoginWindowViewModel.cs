using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using System;
using System.Threading.Tasks;

namespace HackerNews.TUI.ViewModels
{
	public class LoginWindowViewModel : BaseViewModel
	{
		private string _textboxMinWidth;
		public string TextBoxMinWidth
		{
			get { return _textboxMinWidth; }
			set { Set(ref _textboxMinWidth, value); }
		}

		private string _username;
		public string Username
		{
			get { return _username; }
			set
			{
				Set(ref _username, value);
				//LoginCommand.CanExecuteChanged?.Invoke(this, EventArgs.Empty); // TODO: determine how to use WPF ICommand
			}
		}

		private string _password;
		public string Password
		{
			get { return _password; }
			set { Set(ref _password, value); }
		}

		public ConsoleFramework.Events.ICommand LoginCommand { get; init; }

		public LoginWindowViewModel()
		{
			TextBoxMinWidth = "40";
			LoginCommand = new DelegateCommand(Login, CanLogin);
		}

		public bool CanLogin() => true;
		//!(string.IsNullOrEmpty(Username) || Password?.Length <= 0);

		private void Login(object _ = null)
		{
		}

	}
}
