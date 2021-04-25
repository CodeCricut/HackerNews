using Hackernews.WPF.Helpers;
using Hackernews.WPF.Messages.Application;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow.Profile;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class ProfileViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;

		public ICommand LogoutCommand { get; }

		public PrivateUserViewModel PrivateUserViewModel { get; }


		public ProfileViewModel(IEventAggregator ea, PrivateUserViewModel privateUserViewModel)
		{
			_ea = ea;

			PrivateUserViewModel = privateUserViewModel;

			LogoutCommand = new DelegateCommand(Logout);
		}

		private void Logout(object _ = null)
		{
			_ea.SendMessage(new MainWindowSwitchToLoginWindowMessage());
			_ea.PostMessage(new LogoutRequestedMessage());
		}
	}
}
