using Hackernews.WPF.ViewModels;

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
