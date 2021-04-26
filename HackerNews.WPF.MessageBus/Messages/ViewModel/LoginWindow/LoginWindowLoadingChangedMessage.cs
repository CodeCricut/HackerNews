namespace HackerNews.WPF.MessageBus.Messages.ViewModel.LoginWindow
{
	public sealed class LoginWindowLoadingChangedMessage
	{
		public LoginWindowLoadingChangedMessage(bool isLoading)
		{
			IsLoading = isLoading;
		}

		public bool IsLoading { get; }
	}
}
