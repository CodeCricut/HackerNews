using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class ProfileViewModel : BaseViewModel
	{
		public Action LogoutAction { get; set; }
		public ICommand LogoutCommand { get; }


		public ProfileViewModel(PrivateUserViewModel privateUserViewModel)
		{
			PrivateUserViewModel = privateUserViewModel;
			LogoutCommand = new DelegateCommand(_ => LogoutAction?.Invoke());
		}

		public PrivateUserViewModel PrivateUserViewModel { get; }
	}
}
