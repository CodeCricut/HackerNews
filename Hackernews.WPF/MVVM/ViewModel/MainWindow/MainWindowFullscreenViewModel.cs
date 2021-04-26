using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.ViewModel.MainWindow;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class MainWindowFullscreenViewModel : BaseViewModel
	{
		#region Fullscreen VMs
		public bool NotInFullscreenMode { get => SelectedFullscreenViewModel == null; }

		private object _selectedFullscreenViewModel;
		private readonly IEventAggregator _ea;

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

		public MainWindowFullscreenViewModel(IEventAggregator ea,
			HomeViewModel homeVm,
			ProfileViewModel profileVm,
			SettingsViewModel settingsVm)
		{
			_ea = ea;


			HomeViewModel = homeVm;
			ProfileViewModel = profileVm;
			SettingsViewModel = settingsVm;


			SelectHomeCommand = new DelegateCommand(SelectHome);
			SelectProfileCommand = new DelegateCommand(SelectProfile);
			SelectSettingsCommand = new DelegateCommand(SelectSettings);

			ea.RegisterHandler<FullscreenDeselectedMessage>(msg => DeselectFullscreenVM());
		}

		public void SelectHome(object parameter = null)
		{
			_ea.SendMessage(new EntityDeselectedMessage());
			SelectedFullscreenViewModel = HomeViewModel;
		}

		public void SelectProfile(object parameter = null)
		{
			_ea.SendMessage(new EntityDeselectedMessage());

			SelectedFullscreenViewModel = ProfileViewModel;

			ProfileViewModel.LoadProfileCommand.Execute();
		}

		public void SelectSettings(object parameter = null)
		{
			_ea.SendMessage(new EntityDeselectedMessage());

			SelectedFullscreenViewModel = SettingsViewModel;
		}

		public void DeselectFullscreenVM() => SelectedFullscreenViewModel = null;
	}
}
