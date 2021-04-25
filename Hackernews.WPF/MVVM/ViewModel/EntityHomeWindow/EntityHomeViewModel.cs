using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Articles;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class EntityHomeViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVM;

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

		public ICommand CloseCommand { get; }

		public EntityHomeViewModel(IEventAggregator ea, 
			IViewManager viewManager, 
			IApiClient apiClient, 
			PrivateUserViewModel userVM)
		{
			_ea = ea;
			_viewManager = viewManager;
			_apiClient = apiClient;
			_userVM = userVM;

			CloseCommand = new DelegateCommand(_ => _viewManager.Close(this));
		}

		public void ShowBoardHome(BoardViewModel boardVm)
		{
			SelectBoardVM(boardVm);

			OpenWindow();
		}

		public void ShowArticleHome(ArticleViewModel articleVm)
		{
			SelectArticleVM(articleVm);

			OpenWindow();
		}

		private void OpenWindow(object _ = null) => _viewManager.Show(this);

		private void SelectBoardVM(BoardViewModel boardVm)
		{
			// Copy the board vm to keep it always selected
			var newBoardVm = new BoardViewModel(_ea, _viewManager, _apiClient, _userVM)
			{
				Board = boardVm.Board,
				IsSelected = true
			};

			BoardHomeViewModel boardHomeVM = new BoardHomeViewModel(_ea, _viewManager, newBoardVm, _apiClient, _userVM);
			boardHomeVM.LoadBoardCommand.Execute();

			SelectedHomeViewModel = boardHomeVM;
		}

		private void SelectArticleVM(ArticleViewModel articleVm)
		{
			// Copy the article vm reference to keep it always selected.
			var newArticleVm = new ArticleViewModel(_ea, _viewManager, _userVM, _apiClient)
			{
				Article = articleVm.Article,
				IsSelected = true
			};
			newArticleVm.LoadEntityCommand.Execute();

			ArticleHomeViewModel articleHomeVm = new ArticleHomeViewModel(newArticleVm, _apiClient);
			articleHomeVm.LoadArticleCommand.Execute();

			SelectedHomeViewModel = articleHomeVm;
		}
	}
}
