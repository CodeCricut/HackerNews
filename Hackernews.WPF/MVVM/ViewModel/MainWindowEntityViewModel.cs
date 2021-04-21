using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class MainWindowEntityViewModel : BaseViewModel
	{
		#region List VMs
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

		public BoardsListViewModel BoardListViewModel { get; }
		public ArticleListViewModel ArticleListViewModel { get; }
		public CommentListViewModel CommentListViewModel { get; }
		public UserListViewModel UserListViewModel { get; }
		#endregion

		#region Details VMs
		private object _selectedDetailsViewModel;
		private readonly MainWindowViewModel _mainWindowVM;
		private readonly IApiClient _apiClient;

		public object SelectedDetailsViewModel
		{
			get { return _selectedDetailsViewModel; }
			set
			{
				_selectedDetailsViewModel = value;
				RaisePropertyChanged();
			}
		}

		public BoardViewModel BoardViewModel { get; }
		public ArticleViewModel ArticleViewModel { get; }
		public CommentViewModel CommentViewModel { get; }
		public PublicUserViewModel PublicUserViewModel { get; }
		#endregion


		public AsyncDelegateCommand SelectUsersCommand { get; }
		public ICommand SelectBoardsCommand { get; }
		public ICommand SelectArticlesCommand { get; }
		public ICommand SelectCommentsCommand { get; }

		public MainWindowEntityViewModel(MainWindowViewModel mainWindowVM, IApiClient apiClient)
		{
			_mainWindowVM = mainWindowVM;
			_apiClient = apiClient;

			UserListViewModel = new UserListViewModel(apiClient);
			BoardListViewModel = new BoardsListViewModel(apiClient, mainWindowVM.PrivateUserViewModel);
			ArticleListViewModel = new ArticleListViewModel(vm => new LoadArticlesCommand(vm, apiClient, mainWindowVM.PrivateUserViewModel));
			CommentListViewModel = new CommentListViewModel(vm => new LoadCommentsCommand(vm, apiClient));

			PublicUserViewModel = new PublicUserViewModel(apiClient);
			BoardViewModel = new BoardViewModel(apiClient, mainWindowVM.PrivateUserViewModel);
			ArticleViewModel = new ArticleViewModel(mainWindowVM.PrivateUserViewModel, apiClient);
			CommentViewModel = new CommentViewModel();

			SelectUsersCommand = new AsyncDelegateCommand(SelectUsersAsync);
			SelectBoardsCommand = new AsyncDelegateCommand(SelectBoardsAsync);
			SelectArticlesCommand = new AsyncDelegateCommand(SelectArticlesAsync);
			SelectCommentsCommand = new AsyncDelegateCommand(SelectCommentsAsync);
		}

		public async Task SelectUsersAsync(object parameter = null)
		{
			SelectedListViewModel = UserListViewModel;
			SelectedDetailsViewModel = PublicUserViewModel;
			_mainWindowVM.DeselectFullscreenVM();
			await Task.Factory.StartNew(() => UserListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectBoardsAsync(object parameter = null)
		{
			SelectedListViewModel = BoardListViewModel;
			SelectedDetailsViewModel = BoardViewModel; 
			_mainWindowVM.DeselectFullscreenVM();

			await Task.Factory.StartNew(() => BoardListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectArticlesAsync(object parameter = null)
		{
			SelectedListViewModel = ArticleListViewModel;
			SelectedDetailsViewModel = ArticleViewModel;
			_mainWindowVM.DeselectFullscreenVM();

			await Task.Factory.StartNew(() => ArticleListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectCommentsAsync(object parameter = null)
		{
			SelectedListViewModel = CommentListViewModel;
			SelectedDetailsViewModel = CommentViewModel;
			_mainWindowVM.DeselectFullscreenVM();

			await Task.Factory.StartNew(() => CommentListViewModel.LoadCommand.TryExecute());
		}

		public void DeselectEntityVM()
		{
			SelectedDetailsViewModel = null;
			SelectedListViewModel = null;
		}
	}
}
