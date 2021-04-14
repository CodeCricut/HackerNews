using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.ViewModels
{
	public class PrivateUserViewModel : BaseViewModel
	{
		private readonly IApiClient _apiClient;

		#region User and User Props
		private GetPrivateUserModel _user = new GetPrivateUserModel();
		private GetPrivateUserModel User
		{
			get => _user; set
			{
				_user = value;
				RaisePropertyChanged("");
			}
		}

		public bool IsLoaded { get => User != null; }

		public string UserName
		{
			get => User?.UserName ?? "";
			set
			{
				if (!User?.UserName.Equals(value) ?? false)
				{
					User.UserName = value;
					RaisePropertyChanged();
				}
			}
		}

		public int Karma
		{
			get => User?.Karma ?? 0;
			set
			{
				if (User.Karma != value)
				{
					User.Karma = value;
					RaisePropertyChanged();
				}
			}
		}

		public DateTime JoinDate
		{
			get => User?.JoinDate ?? new DateTime(0);
			set
			{
				if (User?.JoinDate != value)
				{
					User.JoinDate = value;
					RaisePropertyChanged();
				}
			}
		}

		public DateTimeOffset JoinDateOffset
		{
			get => User?.JoinDate ?? JoinDate;
			set
			{
				if (User?.JoinDate != value.DateTime)
				{
					User.JoinDate = value.DateTime;
					RaisePropertyChanged();
				}
			}
		}

		public IEnumerable<int> ArticleIds { get => User?.ArticleIds; }
		public IEnumerable<int> SavedArticleIds { get => User?.SavedArticles; }
		public IEnumerable<int> CommentIds { get => User?.CommentIds; }
		public IEnumerable<int> SavedCommentIds { get => User?.SavedComments; }
		public IEnumerable<int> BoardModeratingIds { get => User?.BoardsModerating; }
		public IEnumerable<int> BoardSubcribedIds { get => User?.BoardsSubscribed; }
		#endregion

		public BoardsListViewModel BoardsModeratingListViewModel { get; private set; }
		public BoardsListViewModel BoardsSubscribedListViewModel { get; private set; }

		public ArticleListViewModel ArticlesWrittenListViewModel { get; private set; }
		public ArticleListViewModel ArticlesSavedListViewModel { get; private set; }

		public CommentListViewModel CommentsWrittenListViewModel { get; private set; }
		public CommentListViewModel CommentsSavedListViewModel { get; private set; }

		public AsyncDelegateCommand TryLoadUserCommand { get; }

		public PrivateUserViewModel(IApiClient apiClient)
		{
			_apiClient = apiClient;
			InstantiateOwnedViewModels();

			TryLoadUserCommand = new AsyncDelegateCommand(TryLoadPrivateUserAsync);
		}

		private void InstantiateOwnedViewModels()
		{
			CreateBaseCommand<BoardsListViewModel> createLoadBoardsByIdCommand = vm => new LoadBoardsByIdsCommand(vm, _apiClient);
			BoardsModeratingListViewModel = new BoardsListViewModel(_apiClient, createLoadBoardsByIdCommand);
			BoardsSubscribedListViewModel = new BoardsListViewModel(_apiClient, createLoadBoardsByIdCommand);

			CreateBaseCommand<ArticleListViewModel> createLoadArticlesByIdCommand = vm => new LoadArticlesByIdsCommand(vm, _apiClient, this);
			ArticlesWrittenListViewModel = new ArticleListViewModel(createLoadArticlesByIdCommand);
			ArticlesSavedListViewModel = new ArticleListViewModel(createLoadArticlesByIdCommand);

			CreateBaseCommand<CommentListViewModel> createLoadCommentsByIdCommand = vm => new LoadCommentsByIdsCommand(vm, _apiClient);
			CommentsWrittenListViewModel = new CommentListViewModel(createLoadCommentsByIdCommand);
			CommentsSavedListViewModel = new CommentListViewModel(createLoadCommentsByIdCommand);
		}

		//private void SetupBoardsModeratingListVM()
		//{
		//	BoardsModeratingListViewModel = new BoardsListViewModel(_apiClient);
		//	var loadModeratingBoardsCommand = new LoadBoardsByIdsCommand(BoardsModeratingListViewModel, _apiClient);
		//	BoardsModeratingListViewModel.LoadCommand = loadModeratingBoardsCommand;
		//}

		//private void SetupBoardsSubscribedListVM()
		//{
		//	BoardsSubscribedListViewModel = new BoardsListViewModel(_apiClient);
		//	var loadSubscribignBoardsCommand = new LoadBoardsByIdsCommand(BoardsSubscribedListViewModel, _apiClient);
		//	BoardsSubscribedListViewModel.LoadCommand = loadSubscribignBoardsCommand;
		//}

		//private void SetupArticlesWrittenListVM()
		//{
		//	ArticlesWrittenListViewModel = new ArticleListViewModel(_apiClient, this);
		//	var loadWrittenArticlesCOmmand = new LoadArticlesByIdsCommand(ArticlesWrittenListViewModel, _apiClient, this);
		//	ArticlesWrittenListViewModel.LoadCommand = loadWrittenArticlesCOmmand;
		//}

		//private void SetupArticlesSavedListVM()
		//{
		//	ArticlesSavedListViewModel = new ArticleListViewModel(_apiClient, this);
		//	var loadSavedArticlesCommand = new LoadArticlesByIdsCommand(ArticlesSavedListViewModel, _apiClient, this);
		//	ArticlesSavedListViewModel.LoadCommand = loadSavedArticlesCommand;
		//}

		//private void SetupCommentsWrittenListVM()
		//{
		//	CommentsWrittenListViewModel = new CommentListViewModel(_apiClient);
		//	var loadWrittenCommentsCommand = new LoadCommentsByIdsCommand(CommentsWrittenListViewModel, _apiClient);
		//	CommentsWrittenListViewModel.LoadCommand = loadWrittenCommentsCommand;
		//}

		//private void SetupCommentsSavedListVM()
		//{
		//	CommentsSavedListViewModel = new CommentListViewModel(_apiClient);
		//	var loadSavedCommentsCommand = new LoadCommentsByIdsCommand(CommentsSavedListViewModel, _apiClient);
		//	CommentsSavedListViewModel.LoadCommand = loadSavedCommentsCommand;
		//}


		private async Task TryLoadPrivateUserAsync(object parameter = null)
		{
			User = await _apiClient.GetAsync<GetPrivateUserModel>("users/me");
		    await LoadOwnedViewModelsAsync();
		}

		private async Task LoadOwnedViewModelsAsync()
		{
			await Task.Factory.StartNew(() =>
			{
				BoardsModeratingListViewModel.LoadCommand.TryExecute(User.BoardsModerating);
				BoardsSubscribedListViewModel.LoadCommand.TryExecute(User.BoardsSubscribed);

				ArticlesWrittenListViewModel.LoadCommand.TryExecute(User.ArticleIds);
				ArticlesSavedListViewModel.LoadCommand.TryExecute(User.SavedArticles);

				CommentsWrittenListViewModel.LoadCommand.TryExecute(User.CommentIds);
				CommentsSavedListViewModel.LoadCommand.TryExecute(User.SavedComments);
			});
		}
	}
}
