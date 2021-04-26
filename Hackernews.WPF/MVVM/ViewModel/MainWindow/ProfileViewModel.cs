using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class ProfileViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;

		public ICommand LogoutCommand { get; }

		public PrivateUserViewModel PrivateUserViewModel { get; }

		public EntityListViewModel<BoardViewModel, GetBoardModel> BoardsModeratingListViewModel { get; }
		public EntityListViewModel<BoardViewModel, GetBoardModel> BoardsSubscribedListViewModel { get; }

		public EntityListViewModel<ArticleViewModel, GetArticleModel> ArticlesWrittenListViewModel { get; }
		public EntityListViewModel<ArticleViewModel, GetArticleModel> ArticlesSavedListViewModel { get; }

		public EntityListViewModel<CommentViewModel, GetCommentModel> CommentsWrittenListViewModel { get; }
		public EntityListViewModel<CommentViewModel, GetCommentModel> CommentsSavedListViewModel { get; }

		public AsyncDelegateCommand LoadProfileCommand { get; }

		public ProfileViewModel(IEventAggregator ea,
			PrivateUserViewModel privateUserViewModel,

			IBoardListViewModelFactory boardListViewModelFactory,
			IArticleListViewModelFactory articleListViewModelFactory,
			ICommentListViewModelFactory commentListViewModelFactory)
		{
			_ea = ea;
			PrivateUserViewModel = privateUserViewModel;

			BoardsModeratingListViewModel = boardListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			BoardsSubscribedListViewModel = boardListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);

			ArticlesWrittenListViewModel = articleListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			ArticlesSavedListViewModel = articleListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			CommentsWrittenListViewModel = commentListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			CommentsSavedListViewModel = commentListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);

			LogoutCommand = new DelegateCommand(Logout);
			LoadProfileCommand = new AsyncDelegateCommand(LoadProfileAsync);
		}

		private void Logout(object _ = null)
		{
			_ea.SendMessage(new MainWindowSwitchToLoginWindowMessage());
			_ea.PostMessage(new LogoutRequestedMessage());
		}

		private async Task LoadProfileAsync(object parameter = null)
		{
			await PrivateUserViewModel.LoadUserAsync();
			await LoadOwnedViewModelsAsync();
		}

		private async Task LoadOwnedViewModelsAsync()
		{
			await Task.Factory.StartNew(() =>
			{
				BoardsModeratingListViewModel.LoadCommand.TryExecute(PrivateUserViewModel.User.BoardsModerating);
				BoardsSubscribedListViewModel.LoadCommand.TryExecute(PrivateUserViewModel.User.BoardsSubscribed);

				ArticlesWrittenListViewModel.LoadCommand.TryExecute(PrivateUserViewModel.User.ArticleIds);
				ArticlesSavedListViewModel.LoadCommand.TryExecute(PrivateUserViewModel.User.SavedArticles);

				CommentsWrittenListViewModel.LoadCommand.TryExecute(PrivateUserViewModel.User.CommentIds);
				CommentsSavedListViewModel.LoadCommand.TryExecute(PrivateUserViewModel.User.SavedComments);
			});
		}
	}
}
