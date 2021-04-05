using Hackernews.WPF.Helpers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public enum NavigationModelType
	{
		Users,
		Boards,
		Articles,
		Comments
	}

	public class NavigationViewModel : BaseViewModel
	{
		public NavigationViewModel(UserListViewModel userListViewModel, BoardsListViewModel boardsListViewModel, 
			ArticlesViewModel articlesViewModel, CommentListViewModel commentListViewModel)
		{
			NavigationModelType = NavigationModelType.Users;

			SelectUsersCommand = new AsyncDelegateCommand(SelectUsersAsync);
			SelectBoardsCommand = new AsyncDelegateCommand(SelectBoardsAsync);
			SelectArticlesCommand = new AsyncDelegateCommand(SelectArticlesAsync);
			SelectCommentsCommand = new AsyncDelegateCommand(SelectCommentsAsync);

			_userListViewModel = userListViewModel;
			_boardsListViewModel = boardsListViewModel;
			_articlesViewModel = articlesViewModel;
			_commentListViewModel = commentListViewModel;
		}

		#region Public Properties
		public AsyncDelegateCommand SelectUsersCommand { get; }
		public ICommand SelectBoardsCommand { get; }
		public ICommand SelectArticlesCommand { get; }
		public ICommand SelectCommentsCommand { get; }

		private readonly UserListViewModel _userListViewModel;
		private readonly BoardsListViewModel _boardsListViewModel;
		private readonly ArticlesViewModel _articlesViewModel;
		private readonly CommentListViewModel _commentListViewModel;

		private NavigationModelType _navigationModelType = NavigationModelType.Users;
		public NavigationModelType NavigationModelType
		{
			get => _navigationModelType;
			set
			{
				if (_navigationModelType != value)
				{
					_navigationModelType = value;
					RaisePropertyChanged("");
				}
			}
		}

		public bool AreUsersSelected	{ get => NavigationModelType == NavigationModelType.Users; }
		public bool AreBoardsSelected	{ get => NavigationModelType == NavigationModelType.Boards; }
		public bool AreArticlesSelected { get => NavigationModelType == NavigationModelType.Articles; }
		public bool AreCommentsSelected { get => NavigationModelType == NavigationModelType.Comments; }
		#endregion

		public async Task SelectUsersAsync()
		{
			NavigationModelType = NavigationModelType.Users;
			await _userListViewModel.LoadUsersAsync();
		}

		public async Task SelectBoardsAsync()
		{
			NavigationModelType = NavigationModelType.Boards;
			await _boardsListViewModel.LoadBoardsAsync();
		}

		public async Task SelectArticlesAsync()
		{
			NavigationModelType = NavigationModelType.Articles;
			await _articlesViewModel.LoadArticlesAsync();
		}

		public async Task SelectCommentsAsync()
		{
			NavigationModelType = NavigationModelType.Comments;
			await _commentListViewModel.LoadCommentsAsync();
		}
	}
}
