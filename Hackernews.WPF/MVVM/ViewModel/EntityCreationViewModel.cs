using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
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
				RaisePropertyChanged(nameof(LoginModelSelected));
			}
		}

		public bool LoginModelSelected => SelectedViewModel == BoardCreationViewModel;

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

		private void OpenWindow(object parameter = null)
		{
			App.Current.Dispatcher.Invoke(() =>
			{
				if (_entityCreationWindow == null || _entityCreationWindow.IsClosed)
				{
					// Create and show new window if already disposed
					_entityCreationWindow = new EntityCreationWindow(this);
				}
				_entityCreationWindow.Show();
			});
		}

		// TODO: For some reason, the BoardCreationViewModel.CreateBoardCommand.Invoke calls this without regard to what it is supposed to do...
		private void CloseWindow(object parameter = null)
		{
			App.Current.Dispatcher.Invoke(() =>
			{
				// Close if not disposed
				if (_entityCreationWindow != null && !_entityCreationWindow.IsClosed)
				{
					_entityCreationWindow.Close();
				}
			});
		}

		public EntityCreationViewModel(IApiClient apiClient)
		{
			BoardCreationViewModel = new BoardCreationViewModel(this, apiClient);
			_entityCreationWindow = new EntityCreationWindow(this);

			ShowBoardCreationWindowCommand = new DelegateCommand(ShowBoardCreationWindow);
			//OpenCommand = new DelegateCommand(OpenWindow);
			CloseCommand = new DelegateCommand(CloseWindow);

			SelectedViewModel = BoardCreationViewModel;
		}
	}
}
