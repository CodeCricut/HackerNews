using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.MessageBus.Core;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Messages.ViewModel.MainWindow;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class MainWindowEntityViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;

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

		public object SelectedDetailsViewModel
		{
			get { return _selectedDetailsViewModel; }
			set
			{
				_selectedDetailsViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(AreBoardsSelected));
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

		public bool AreBoardsSelected { get => SelectedDetailsViewModel == BoardViewModel; }

		public MainWindowEntityViewModel(IEventAggregator ea,

			PublicUserViewModel publicUserVm,
			BoardViewModel boardVm,
			ArticleViewModel articleVm,
			CommentViewModel commentVm,

			IUserListViewModelFactory userListViewModelFactory,
			IBoardListViewModelFactory boardListViewModelFactory,
			IArticleListViewModelFactory articleListViewModelFactory,
			ICommentListViewModelFactory commentListViewModelFactory
			)
		{
			_ea = ea;

			PublicUserViewModel = publicUserVm;
			BoardViewModel = boardVm;
			ArticleViewModel = articleVm;
			CommentViewModel = commentVm;

			UserListViewModel = userListViewModelFactory.Create(LoadEntityListEnum.LoadAll);
			BoardListViewModel = boardListViewModelFactory.Create(LoadEntityListEnum.LoadAll);
			ArticleListViewModel = articleListViewModelFactory.Create(LoadEntityListEnum.LoadAll);
			CommentListViewModel = commentListViewModelFactory.Create(LoadEntityListEnum.LoadAll);

			SelectUsersCommand = new AsyncDelegateCommand(SelectUsersAsync);
			SelectBoardsCommand = new AsyncDelegateCommand(SelectBoardsAsync);
			SelectArticlesCommand = new AsyncDelegateCommand(SelectArticlesAsync);
			SelectCommentsCommand = new AsyncDelegateCommand(SelectCommentsAsync);

			ea.RegisterHandler<EntityDeselectedMessage>(msg => DeselectEntityVM());
		}

		public async Task SelectUsersAsync(object parameter = null)
		{
			SelectedListViewModel = UserListViewModel;
			SelectedDetailsViewModel = PublicUserViewModel;

			_ea.SendMessage(new FullscreenDeselectedMessage());
			await Task.Factory.StartNew(() => UserListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectBoardsAsync(object parameter = null)
		{
			SelectedListViewModel = BoardListViewModel;
			SelectedDetailsViewModel = BoardViewModel;

			_ea.SendMessage(new FullscreenDeselectedMessage());

			await Task.Factory.StartNew(() => BoardListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectArticlesAsync(object parameter = null)
		{
			SelectedListViewModel = ArticleListViewModel;
			SelectedDetailsViewModel = ArticleViewModel;

			_ea.SendMessage(new FullscreenDeselectedMessage());

			await Task.Factory.StartNew(() => ArticleListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectCommentsAsync(object parameter = null)
		{
			SelectedListViewModel = CommentListViewModel;
			SelectedDetailsViewModel = CommentViewModel;

			_ea.SendMessage(new FullscreenDeselectedMessage());

			await Task.Factory.StartNew(() => CommentListViewModel.LoadCommand.TryExecute());
		}

		public void DeselectEntityVM()
		{
			SelectedDetailsViewModel = null;
			SelectedListViewModel = null;
		}
	}
}
