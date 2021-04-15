using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Comments;
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
		public ICommand SelectProfileCommand { get; }

		public AsyncDelegateCommand SelectUsersCommand { get; }
		public ICommand SelectBoardsCommand { get; }
		public ICommand SelectArticlesCommand { get; }
		public ICommand SelectCommentsCommand { get; }

		public BoardsListViewModel BoardsListViewModel { get; }
		public ArticleListViewModel ArticleListViewModel { get; }
		public CommentListViewModel CommentListViewModel { get; }
		public UserListViewModel UserListViewModel { get; }

		public HomeViewModel HomeViewModel { get; }
		public ProfileViewModel ProfileViewModel { get; }

		public BoardViewModel BoardViewModel { get; }
		public ArticleViewModel ArticleViewModel { get; }
		public CommentViewModel CommentViewModel { get; }
		public PublicUserViewModel PublicUserViewModel { get; }

		//public NavigationViewModel NavigationViewModel { get; }
		public PrivateUserViewModel PrivateUserViewModel { get; }

		public MainWindowViewModel(IApiClient apiClient, PrivateUserViewModel userVM)
		{
			PrivateUserViewModel = new PrivateUserViewModel(apiClient);

			UserListViewModel = new UserListViewModel(apiClient);
			BoardsListViewModel = new BoardsListViewModel(apiClient);
			ArticleListViewModel = new ArticleListViewModel(vm => new LoadArticlesCommand(vm, apiClient, userVM));
			CommentListViewModel = new CommentListViewModel(vm => new LoadCommentsCommand(vm, apiClient));

			HomeViewModel = new HomeViewModel();
			ProfileViewModel = new ProfileViewModel(PrivateUserViewModel);

			PublicUserViewModel = new PublicUserViewModel();
			BoardViewModel = new BoardViewModel();
			ArticleViewModel = new ArticleViewModel(userVM);
			CommentViewModel = new CommentViewModel();

			//NavigationViewModel = new NavigationViewModel(UserListViewModel, BoardsListViewModel, ArticleListViewModel, CommentListViewModel);


			CloseCommand = new DelegateCommand(() => CloseAction?.Invoke());

			SelectHomeCommand = new DelegateCommand(SelectHome);
			SelectProfileCommand = new DelegateCommand(SelectProfile);

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

		public void SelectProfile()
		{
			SelectedListViewModel = null;
			SelectedDetailsViewModel = null;
			SelectedFullscreenViewModel = ProfileViewModel;

			PrivateUserViewModel.TryLoadUserCommand.Execute(null);
		}

		public async Task SelectUsersAsync(object parameter = null)
		{
			SelectedListViewModel = UserListViewModel;
			SelectedDetailsViewModel = PublicUserViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => UserListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectBoardsAsync(object parameter = null)
		{
			SelectedListViewModel = BoardsListViewModel;
			SelectedDetailsViewModel = BoardViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => BoardsListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectArticlesAsync(object parameter = null)
		{
			SelectedListViewModel = ArticleListViewModel;
			SelectedDetailsViewModel = ArticleViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => ArticleListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectCommentsAsync(object parameter = null)
		{
			SelectedListViewModel = CommentListViewModel;
			SelectedDetailsViewModel = CommentViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => CommentListViewModel.LoadCommand.TryExecute());
		}
	}
}
