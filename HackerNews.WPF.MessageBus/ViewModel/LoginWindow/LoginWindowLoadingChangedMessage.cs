using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.WPF.MessageBus.ViewModel.LoginWindow
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
