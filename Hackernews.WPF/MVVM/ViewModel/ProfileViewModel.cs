using Hackernews.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class ProfileViewModel : BaseViewModel
	{
		public ProfileViewModel(PrivateUserViewModel privateUserViewModel)
		{
			PrivateUserViewModel = privateUserViewModel;
		}

		public PrivateUserViewModel PrivateUserViewModel { get; }
	}
}
