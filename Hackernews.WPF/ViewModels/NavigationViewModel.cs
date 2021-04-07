//using Hackernews.WPF.Helpers;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace Hackernews.WPF.ViewModels
//{
//	//public enum NavigationModelType
//	//{
//	//	Users,
//	//	Boards,
//	//	Articles,
//	//	Comments
//	//}

//	public class NavigationViewModel : BaseViewModel
//	{
//		//private object _selectedListViewModel;

//		//public object SelectedListViewModel
//		//{
//		//	get { return _selectedListViewModel; }
//		//	set { 
//		//		_selectedListViewModel = value;
//		//		RaisePropertyChanged();
//		//	}
//		//}

//		//private object _selectedDetailsViewModel;

//		//public object SelectedDetailsViewModel
//		//{
//		//	get { return _selectedDetailsViewModel; }
//		//	set
//		//	{
//		//		_selectedDetailsViewModel = value;
//		//		RaisePropertyChanged();
//		//	}
//		//}


//		public NavigationViewModel(UserListViewModel userListViewModel, BoardsListViewModel boardsListViewModel, 
//			ArticlesViewModel articlesViewModel, CommentListViewModel commentListViewModel)
//		{
//			//NavigationModelType = NavigationModelType.Users;

//			//SelectUsersCommand = new AsyncDelegateCommand(SelectUsersAsync);
//			//SelectBoardsCommand = new AsyncDelegateCommand(SelectBoardsAsync);
//			//SelectArticlesCommand = new AsyncDelegateCommand(SelectArticlesAsync);
//			//SelectCommentsCommand = new AsyncDelegateCommand(SelectCommentsAsync);

//			_userListViewModel = userListViewModel;
//			_boardsListViewModel = boardsListViewModel;
//			_articlesViewModel = articlesViewModel;
//			_commentListViewModel = commentListViewModel;
//		}

//		#region Public Properties
//		//public AsyncDelegateCommand SelectUsersCommand { get; }
//		//public ICommand SelectBoardsCommand { get; }
//		//public ICommand SelectArticlesCommand { get; }
//		//public ICommand SelectCommentsCommand { get; }

//		private readonly UserListViewModel _userListViewModel;
//		private readonly BoardsListViewModel _boardsListViewModel;
//		private readonly ArticlesViewModel _articlesViewModel;
//		private readonly CommentListViewModel _commentListViewModel;

//		private readonly PublicUserViewModel _userViewModel;
//		private readonly BoardViewModel _boardViewModel;
//		private readonly ArticleViewModel _articleViewModel;
//		private readonly CommentViewModel _commentViewModel;

//		//private NavigationModelType _navigationModelType = NavigationModelType.Users;
//		//public NavigationModelType NavigationModelType
//		//{
//		//	get => _navigationModelType;
//		//	set
//		//	{
//		//		if (_navigationModelType != value)
//		//		{
//		//			_navigationModelType = value;
//		//			RaisePropertyChanged("");
//		//		}
//		//	}
//		//}

//		//public bool AreUsersSelected	{ get => NavigationModelType == NavigationModelType.Users; }
//		//public bool AreBoardsSelected	{ get => NavigationModelType == NavigationModelType.Boards; }
//		//public bool AreArticlesSelected { get => NavigationModelType == NavigationModelType.Articles; }
//		//public bool AreCommentsSelected { get => NavigationModelType == NavigationModelType.Comments; }
//		#endregion

//		//public async Task SelectUsersAsync()
//		//{
//		//	SelectedListViewModel = _userListViewModel;
//		//	SelectedDetailsViewModel = _public
//		//	//NavigationModelType = NavigationModelType.Users;
//		//	await _userListViewModel.LoadUsersAsync();
//		//}

//		//public async Task SelectBoardsAsync()
//		//{
//		//	SelectedListViewModel = _boardsListViewModel;
//		//	//NavigationModelType = NavigationModelType.Boards;
//		//	await _boardsListViewModel.LoadBoardsAsync();
//		//}

//		//public async Task SelectArticlesAsync()
//		//{
//		//	SelectedListViewModel = _articlesViewModel;
//		//	//NavigationModelType = NavigationModelType.Articles;
//		//	await _articlesViewModel.LoadArticlesAsync();
//		//}

//		//public async Task SelectCommentsAsync()
//		//{
//		//	SelectedListViewModel = _commentListViewModel;
//		//	//NavigationModelType = NavigationModelType.Comments;
//		//	await _commentListViewModel.LoadCommentsAsync();
//		//}
//	}
//}
