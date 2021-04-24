using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.ViewModels;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class EntityCreationViewModel : BaseViewModel
	{
		#region Loading
		private bool _loading;
		public bool Loading
		{
			get { return _loading; }
			set { _loading = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(NotLoading)); }
		}
		public bool NotLoading { get => !Loading; }
		#endregion

		#region User input validation
		private bool _invalidUserInput;
		public bool InvalidUserInput
		{
			get { return _invalidUserInput; }
			set { _invalidUserInput = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ValidUserInput)); }
		}
		public bool ValidUserInput { get => !InvalidUserInput; }
		#endregion

		#region View switcher
		private object _selectedViewModel;

		public object SelectedViewModel
		{
			get => _selectedViewModel; set
			{
				_selectedViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(CreationVMIsSelected));
			}
		}

		public bool CreationVMIsSelected => SelectedViewModel != null;

		public ICommand ShowBoardCreationWindowCommand { get; }

		private void ShowBoardCreationWindow(object parameter = null)
		{
			SelectedViewModel = BoardCreationViewModel;
			OpenWindow(parameter);
		}

		#endregion

		public BoardCreationViewModel BoardCreationViewModel { get; set; }

		private EntityCreationWindow _entityCreationWindow;

		//public ICommand OpenCommand { get; }
		public ICommand CloseCommand { get; }


		// TODO: use view manager
		private void OpenWindow(object parameter = null)
		{
			//App.Current.Dispatcher.Invoke(() =>
			//{
			//	if (_entityCreationWindow == null || _entityCreationWindow.IsClosed)
			//	{
			//		// Create and show new window if already disposed
			//		_entityCreationWindow = new EntityCreationWindow(this);
			//	}
			//	_entityCreationWindow.Show();
			//});
		}

		private void CloseWindow(object parameter = null)
		{
			//App.Current.Dispatcher.Invoke(() =>
			//{
			//	// Close if not disposed
			//	if (_entityCreationWindow != null && !_entityCreationWindow.IsClosed)
			//	{
			//		_entityCreationWindow.Close();
			//	}
			//});
		}

		public EntityCreationViewModel(IApiClient apiClient)
		{
			BoardCreationViewModel = new BoardCreationViewModel(this, apiClient);
			//_entityCreationWindow = new EntityCreationWindow(this);

			ShowBoardCreationWindowCommand = new DelegateCommand(ShowBoardCreationWindow);
			//OpenCommand = new DelegateCommand(OpenWindow);
			CloseCommand = new DelegateCommand(CloseWindow);

			//SelectedViewModel = BoardCreationViewModel;
		}
	}
}
