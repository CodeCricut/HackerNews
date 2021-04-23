using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.EntityHomeWindow;
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

		#endregion

		#region Home window
		private EntityHomeWindow _entityHomeWindow;
		private readonly IEventAggregator _ea;
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVM;

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

		//public BoardHomeViewModel BoardHomeViewModel { get; }

		public EntityHomeViewModel(IEventAggregator ea, IApiClient apiClient, PrivateUserViewModel userVM)
		{
			_apiClient = apiClient;
			_userVM = userVM;

			CloseCommand = new DelegateCommand(CloseWindow);

			ea.RegisterHandler<ShowBoardHomeMessage>(ShowBoardHome);
		}

		private void ShowBoardHome(ShowBoardHomeMessage msg)
		{
			BoardHomeViewModel boardHomeVM = new BoardHomeViewModel(msg.BoardVM, _apiClient, _userVM);
			SelectedHomeViewModel = boardHomeVM;

			OpenWindow();
		}
	}
}
