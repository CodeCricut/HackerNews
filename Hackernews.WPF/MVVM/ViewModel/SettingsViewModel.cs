using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class SettingsViewModel : BaseViewModel
	{

		public bool IsDarkTheme
		{
			get => App.Skin == Skin.Dark;
		}


		public ICommand ToggleSkinCommand { get; }

		public SettingsViewModel()
		{
			ToggleSkinCommand = new DelegateCommand(ToggleSkin);
		}

		private void ToggleSkin(object darkToggled)
		{
			if ((bool)darkToggled)
			{
				// Dark theme toggled
				(App.Current as App).ChangeSkin(Skin.Dark);
			}
			else
			{
				// Light theme toggled
				(App.Current as App).ChangeSkin(Skin.Light);
			}

			RaisePropertyChanged(nameof(IsDarkTheme));
		}
	}
}
