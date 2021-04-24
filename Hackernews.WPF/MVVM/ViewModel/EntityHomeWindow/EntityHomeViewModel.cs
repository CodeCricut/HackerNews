using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.Messages.ViewModel.EntityHomeWindow;
using Hackernews.WPF.MVVM.ViewModel.Articles;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.Services;
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
		private readonly IViewManager _viewManager;
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

		private void CloseWindow(object parameter = null)
		{
			System.Windows.Application.Current.Dispatcher.Invoke(() =>
			{
				// Close if not disposed
				if (_entityHomeWindow != null && !_entityHomeWindow.IsClosed)
					_entityHomeWindow.Close();
			});
		}
		#endregion

		public EntityHomeViewModel(IEventAggregator ea,  IApiClient apiClient, PrivateUserViewModel userVM)
		{
			_ea = ea;
			_apiClient = apiClient;
			_userVM = userVM;

			CloseCommand = new DelegateCommand(CloseWindow);
		}

		public void ShowBoardHome(BoardViewModel boardVm)
		{
			// Copy the board vm to keep it always selected
			var newBoardVm = new BoardViewModel(_ea, _apiClient, _userVM)
			{
				Board = boardVm.Board,
				IsSelected = true
			};

			BoardHomeViewModel boardHomeVM = new BoardHomeViewModel(_ea, newBoardVm, _apiClient, _userVM);
			boardHomeVM.LoadBoardCommand.Execute();

			SelectedHomeViewModel = boardHomeVM;

			OpenWindow();
		}

		public void ShowArticleHome(ArticleViewModel articleVm)
		{
			// Copy the article vm reference to keep it always selected.
			var newArticleVm = new ArticleViewModel(_ea, _userVM, _apiClient)
			{
				Article = articleVm.Article,
				IsSelected = true
			};
			newArticleVm.LoadEntityCommand.Execute();

			ArticleHomeViewModel articleHomeVm = new ArticleHomeViewModel(newArticleVm, _apiClient);
			articleHomeVm.LoadArticleCommand.Execute();

			SelectedHomeViewModel = articleHomeVm;

			OpenWindow();
		}
	}
}
