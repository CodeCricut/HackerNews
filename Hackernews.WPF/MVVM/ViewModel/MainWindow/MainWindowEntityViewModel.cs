using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class MainWindowEntityViewModel : BaseViewModel
	{
		#region List VMs
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

		public EntityListViewModel<BoardViewModel, GetBoardModel> BoardListViewModel { get; }
		public EntityListViewModel<ArticleViewModel, GetArticleModel> ArticleListViewModel { get; }
		public EntityListViewModel<CommentViewModel, GetCommentModel> CommentListViewModel { get; }
		public EntityListViewModel<PublicUserViewModel, GetPublicUserModel> UserListViewModel { get; }
		#endregion

		#region Details VMs
		private object _selectedDetailsViewModel;
		private readonly MainWindowViewModel _mainWindowVM;
		private readonly PrivateUserViewModel _userVM;
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

		public MainWindowEntityViewModel(MainWindowViewModel mainWindowVM,
			PrivateUserViewModel userVM,
			IApiClient apiClient)
		{
			_mainWindowVM = mainWindowVM;
			_userVM = userVM;
			_apiClient = apiClient;

			// Hows that for a class signature + constructor?
			UserListViewModel = new UserListViewModel(createLoadCommand: entityVM => new LoadUsersCommand(entityVM, apiClient));
			BoardListViewModel = new BoardListViewModel(createLoadCommand: entityVM => new LoadBoardsCommand(entityVM, apiClient, userVM));
			ArticleListViewModel = new ArticleListViewModel(createLoadCommand: entityVM => new LoadArticlesCommand(entityVM, userVM, apiClient));
			CommentListViewModel = new CommentListViewModel(createLoadCommand: entityVM => new LoadCommentsCommand(entityVM, apiClient));

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
			_mainWindowVM.FullscreenVM.DeselectFullscreenVM();
			await Task.Factory.StartNew(() => UserListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectBoardsAsync(object parameter = null)
		{
			SelectedListViewModel = BoardListViewModel;
			SelectedDetailsViewModel = BoardViewModel;
			_mainWindowVM.FullscreenVM.DeselectFullscreenVM();

			await Task.Factory.StartNew(() => BoardListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectArticlesAsync(object parameter = null)
		{
			SelectedListViewModel = ArticleListViewModel;
			SelectedDetailsViewModel = ArticleViewModel;
			_mainWindowVM.FullscreenVM.DeselectFullscreenVM();

			await Task.Factory.StartNew(() => ArticleListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectCommentsAsync(object parameter = null)
		{
			SelectedListViewModel = CommentListViewModel;
			SelectedDetailsViewModel = CommentViewModel;
			_mainWindowVM.FullscreenVM.DeselectFullscreenVM();

			await Task.Factory.StartNew(() => CommentListViewModel.LoadCommand.TryExecute());
		}

		public void DeselectEntityVM()
		{
			SelectedDetailsViewModel = null;
			SelectedListViewModel = null;
		}
	}
}
