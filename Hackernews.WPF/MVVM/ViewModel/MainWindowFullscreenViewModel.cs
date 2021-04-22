using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class MainWindowFullscreenViewModel : BaseViewModel
	{
		#region Fullscreen VMs
		public bool NotInFullscreenMode { get => SelectedFullscreenViewModel == null; }

		private object _selectedFullscreenViewModel;
		private readonly MainWindowViewModel _mainWindowVM;

		public object SelectedFullscreenViewModel
		{
			get => _selectedFullscreenViewModel;
			set
			{
				_selectedFullscreenViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(NotInFullscreenMode));
			}
		}

		public HomeViewModel HomeViewModel { get; }
		public ProfileViewModel ProfileViewModel { get; }
		public SettingsViewModel SettingsViewModel { get; }
		#endregion

		public ICommand SelectHomeCommand { get; }
		public ICommand SelectProfileCommand { get; }
		public ICommand SelectSettingsCommand { get; }

		public MainWindowFullscreenViewModel(MainWindowViewModel mainWindowVM)
		{
			HomeViewModel = new HomeViewModel();
			ProfileViewModel = new ProfileViewModel(mainWindowVM.PrivateUserViewModel)
			{
				LogoutAction = () => mainWindowVM.LogoutAction?.Invoke()
			};
			SettingsViewModel = new SettingsViewModel();


			SelectHomeCommand = new DelegateCommand(SelectHome);
			SelectProfileCommand = new DelegateCommand(SelectProfile);
			SelectSettingsCommand = new DelegateCommand(SelectSettings);
			_mainWindowVM = mainWindowVM;
		}

		public void SelectHome(object parameter = null)
		{
			_mainWindowVM.EntityVM.DeselectEntityVM();
			SelectedFullscreenViewModel = HomeViewModel;
		}

		public void SelectProfile(object parameter = null)
		{
			_mainWindowVM.EntityVM.DeselectEntityVM();

			SelectedFullscreenViewModel = ProfileViewModel;

			// TODO; add load command to profile vm
			_mainWindowVM.PrivateUserViewModel.TryLoadUserCommand.Execute(null);
		}

		public void SelectSettings(object parameter = null)
		{
			_mainWindowVM.EntityVM.DeselectEntityVM();

			SelectedFullscreenViewModel = SettingsViewModel;
		}

		public void DeselectFullscreenVM()
		{
			SelectedFullscreenViewModel = null;
		}
	}
}
