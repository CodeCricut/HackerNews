using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
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


		private IPageNavigatorViewModel _selectedListViewModel;
		public IPageNavigatorViewModel SelectedListViewModel
		{
			get { return _selectedListViewModel; }
			set
			{
				_selectedListViewModel = value;
				RaisePropertyChanged();
			}
		}

		private object _selectedDetailsViewModel;
		public object SelectedDetailsViewModel
		{
			get { return _selectedDetailsViewModel; }
			set
			{
				_selectedDetailsViewModel = value;
				RaisePropertyChanged();
			}
		}

		public Action CloseAction { get; set; }

		public ICommand CloseCommand { get; }

		public ICommand SelectHomeCommand { get; }
		public AsyncDelegateCommand SelectUsersCommand { get; }
		public ICommand SelectBoardsCommand { get; }
		public ICommand SelectArticlesCommand { get; }
		public ICommand SelectCommentsCommand { get; }

		public BoardsListViewModel BoardsListViewModel { get; }
		public ArticleListViewModel ArticleListViewModel { get; }
		public CommentListViewModel CommentListViewModel { get; }
		public UserListViewModel UserListViewModel { get; }

		public HomeViewModel HomeViewModel { get;  }

		public BoardViewModel BoardViewModel { get; }
		public ArticleViewModel ArticleViewModel { get; }
		public CommentViewModel CommentViewModel { get; }
		public PublicUserViewModel PublicUserViewModel { get; }

		//public NavigationViewModel NavigationViewModel { get; }
		public PrivateUserViewModel PrivateUserViewModel { get; }

		public MainWindowViewModel(IApiClient apiClient, PrivateUserViewModel userVM)
		{
			UserListViewModel = new UserListViewModel(apiClient);
			BoardsListViewModel = new BoardsListViewModel(apiClient);
			ArticleListViewModel = new ArticleListViewModel(apiClient, userVM);
			CommentListViewModel = new CommentListViewModel(apiClient);

			HomeViewModel = new HomeViewModel();

			PublicUserViewModel = new PublicUserViewModel();
			BoardViewModel = new BoardViewModel();
			ArticleViewModel = new ArticleViewModel(userVM);
			CommentViewModel = new CommentViewModel();

			//NavigationViewModel = new NavigationViewModel(UserListViewModel, BoardsListViewModel, ArticleListViewModel, CommentListViewModel);

			PrivateUserViewModel = new PrivateUserViewModel(apiClient);

			CloseCommand = new DelegateCommand(() => CloseAction?.Invoke());

			SelectHomeCommand = new DelegateCommand(SelectHome);
			SelectUsersCommand = new AsyncDelegateCommand(SelectUsersAsync);
			SelectBoardsCommand = new AsyncDelegateCommand(SelectBoardsAsync);
			SelectArticlesCommand = new AsyncDelegateCommand(SelectArticlesAsync);
			SelectCommentsCommand = new AsyncDelegateCommand(SelectCommentsAsync);
		}

		public void SelectHome()
		{
			SelectedListViewModel = null;
			SelectedDetailsViewModel = null;
			SelectedFullscreenViewModel = HomeViewModel;
		}

		public async Task SelectUsersAsync()
		{
			SelectedListViewModel = UserListViewModel;
			SelectedDetailsViewModel = PublicUserViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => UserListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectBoardsAsync()
		{
			SelectedListViewModel = BoardsListViewModel;
			SelectedDetailsViewModel = BoardViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => BoardsListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectArticlesAsync()
		{
			SelectedListViewModel = ArticleListViewModel;
			SelectedDetailsViewModel = ArticleViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => ArticleListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectCommentsAsync()
		{
			SelectedListViewModel = CommentListViewModel;
			SelectedDetailsViewModel = CommentViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => CommentListViewModel.LoadCommand.TryExecute());
		}
	}
}
