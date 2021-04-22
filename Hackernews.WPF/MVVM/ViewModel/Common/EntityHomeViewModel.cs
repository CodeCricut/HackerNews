using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.ViewModels;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class EntityHomeViewModel : BaseViewModel
	{
		#region View switcher
		private object _selectedHomeViewModel;
		public object SelectedHomeViewModel
		{
			get => _selectedHomeViewModel;
			set
			{
				_selectedHomeViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(HomeVMIsSelected));
			}
		}
		public bool HomeVMIsSelected => SelectedHomeViewModel != null;

		public ICommand ShowBoardHomeWindowCommand { get; }

		private void ShowBoardHomeWindow(object parameter = null)
		{
			SelectedHomeViewModel = BoardHomeViewModel;
			OpenWindow(parameter);
		}
		#endregion

		#region Home window
		private EntityHomeWindow _entityHomeWindow;

		//public ICommand OpenCommand { get; }
		public ICommand CloseCommand { get; }

		private void OpenWindow(object parameter = null)
		{
			System.Windows.Application.Current.Dispatcher.Invoke(() =>
			{
				if (_entityHomeWindow == null || _entityHomeWindow.IsClosed)
				{
					// Create and show new window if already disposed
					_entityHomeWindow = new EntityHomeWindow(this);
				}
				_entityHomeWindow.Show();
			});
		}

		// TODO: For some reason, the BoardCreationViewModel.CreateBoardCommand.Invoke calls this without regard to what it is supposed to do...
		private void CloseWindow(object parameter = null)
		{
			System.Windows.Application.Current.Dispatcher.Invoke(() =>
			{
				// Close if not disposed
				if (_entityHomeWindow != null && !_entityHomeWindow.IsClosed)
				{
					_entityHomeWindow.Close();
				}
			});
		}
		#endregion

		public BoardHomeViewModel BoardHomeViewModel { get; }

		public EntityHomeViewModel(BoardViewModel boardVM, IApiClient apiClient, PrivateUserViewModel userVM)
		{
			BoardHomeViewModel = new BoardHomeViewModel(boardVM, apiClient, userVM);

			CloseCommand = new DelegateCommand(CloseWindow);
			ShowBoardHomeWindowCommand = new DelegateCommand(ShowBoardHomeWindow);
		}
	}
}
