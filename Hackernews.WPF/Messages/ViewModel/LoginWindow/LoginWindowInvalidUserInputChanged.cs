namespace HackerNews.WPF.MessageBus.Messages.ViewModel.LoginWindow
{
	public sealed class LoginWindowInvalidUserInputChanged
	{
		public LoginWindowInvalidUserInputChanged(bool invalidUserInput)
		{
			InvalidUserInput = invalidUserInput;
		}

		public bool InvalidUserInput { get; }
	}
}
