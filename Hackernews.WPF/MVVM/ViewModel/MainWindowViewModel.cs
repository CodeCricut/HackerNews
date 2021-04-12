using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using HackerNews.Domain.Common.Models.Users;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private object _selectedListViewModel;

		public object SelectedListViewModel
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

		public AsyncDelegateCommand SelectUsersCommand { get; }
		public ICommand SelectBoardsCommand { get; }
		public ICommand SelectArticlesCommand { get; }
		public ICommand SelectCommentsCommand { get; }

		public BoardsListViewModel BoardsListViewModel { get; }
		public ArticleListViewModel ArticleListViewModel { get; }
		public CommentListViewModel CommentListViewModel { get; }
		public UserListViewModel UserListViewModel { get; }

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

			PublicUserViewModel = new PublicUserViewModel();
			BoardViewModel = new BoardViewModel();
			ArticleViewModel = new ArticleViewModel(userVM);
			CommentViewModel = new CommentViewModel();

			//NavigationViewModel = new NavigationViewModel(UserListViewModel, BoardsListViewModel, ArticleListViewModel, CommentListViewModel);

			PrivateUserViewModel = new PrivateUserViewModel(apiClient);


			SelectUsersCommand = new AsyncDelegateCommand(SelectUsersAsync);
			SelectBoardsCommand = new AsyncDelegateCommand(SelectBoardsAsync);
			SelectArticlesCommand = new AsyncDelegateCommand(SelectArticlesAsync);
			SelectCommentsCommand = new AsyncDelegateCommand(SelectCommentsAsync);
		}


		public async Task SelectUsersAsync()
		{
			SelectedListViewModel = UserListViewModel;
			SelectedDetailsViewModel = PublicUserViewModel;
			//NavigationModelType = NavigationModelType.Users;
			await UserListViewModel.LoadUsersAsync();
		}

		public async Task SelectBoardsAsync()
		{
			SelectedListViewModel = BoardsListViewModel;
			SelectedDetailsViewModel = BoardViewModel;
			//NavigationModelType = NavigationModelType.Boards;
			await Task.Factory.StartNew(() => BoardsListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectArticlesAsync()
		{
			SelectedListViewModel = ArticleListViewModel;
			SelectedDetailsViewModel = ArticleViewModel;
			//NavigationModelType = NavigationModelType.Articles;
			await Task.Factory.StartNew(() => ArticleListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectCommentsAsync()
		{
			SelectedListViewModel = CommentListViewModel;
			SelectedDetailsViewModel = CommentViewModel;
			//NavigationModelType = NavigationModelType.Comments;
			await CommentListViewModel.LoadCommentsAsync();
		}
	}
}
