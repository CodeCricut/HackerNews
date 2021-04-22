using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public Action CloseAction { get; set; }
		public ICommand CloseCommand { get; }

		public Action LogoutAction { get; set; }
		public ICommand LogoutCommand { get; }

		public PrivateUserViewModel PrivateUserViewModel { get; }

		#region Select view commands
		public ICommand SelectHomeCommand { get; }
		public ICommand SelectProfileCommand { get; }
		public ICommand SelectSettingsCommand { get; }

		#endregion

		#region Fullscreen VMs
		public bool NotInFullscreenMode { get => SelectedFullscreenViewModel == null; }

		private object _selectedFullscreenViewModel;
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

		public MainWindowEntityViewModel EntityVM { get; }

		#region Windowed vms
		public EntityCreationViewModel EntityCreationViewModel { get; }
	//	public EntityHomeViewModel EntityHomeViewModel { get;}
		#endregion

		public MainWindowViewModel(IApiClient apiClient, PrivateUserViewModel userVM)
		{
			CloseCommand = new DelegateCommand(_ => CloseAction?.Invoke());
			LogoutCommand = new DelegateCommand(_ => LogoutAction?.Invoke());

			PrivateUserViewModel = new PrivateUserViewModel(apiClient);

			HomeViewModel = new HomeViewModel();
			ProfileViewModel = new ProfileViewModel(PrivateUserViewModel)
			{
				LogoutAction = () => this.LogoutAction?.Invoke()
			};
			SettingsViewModel = new SettingsViewModel();

			EntityVM = new MainWindowEntityViewModel(this, apiClient);

			EntityCreationViewModel = new EntityCreationViewModel(apiClient);
			// EntityHomeViewModel = new EntityHomeViewModel(BoardViewModel, apiClient, userVM);

			SelectHomeCommand = new DelegateCommand(SelectHome);
			SelectProfileCommand = new DelegateCommand(SelectProfile);
			SelectSettingsCommand = new DelegateCommand(SelectSettings);

		}

		public void SelectHome(object parameter = null)
		{
			EntityVM.DeselectEntityVM();
			SelectedFullscreenViewModel = HomeViewModel;
		}

		public void SelectProfile(object parameter = null)
		{
			EntityVM.DeselectEntityVM();

			SelectedFullscreenViewModel = ProfileViewModel;

			PrivateUserViewModel.TryLoadUserCommand.Execute(null);
		}

		public void SelectSettings(object parameter = null)
		{
			EntityVM.DeselectEntityVM();

			SelectedFullscreenViewModel = SettingsViewModel;
		}

		public void DeselectFullscreenVM()
		{
			SelectedFullscreenViewModel = null;
		}
	}
}
