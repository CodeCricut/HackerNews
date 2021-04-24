using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Factories;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hackernews.WPF.ViewModels
{
	public class PrivateUserViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly ILoadEntityCommandFactoryPrincipal _loadCommandFactoryPrincipal;

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

		private BitmapImage _userImage;

		public BitmapImage UserImage
		{
			get { return _userImage; }
			set { _userImage = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(HasImage)); }
		}

		public bool HasImage { get => UserImage != null; }


		public IEnumerable<int> ArticleIds { get => User?.ArticleIds; }
		public IEnumerable<int> SavedArticleIds { get => User?.SavedArticles; }
		public IEnumerable<int> CommentIds { get => User?.CommentIds; }
		public IEnumerable<int> SavedCommentIds { get => User?.SavedComments; }
		public IEnumerable<int> BoardModeratingIds { get => User?.BoardsModerating; }
		public IEnumerable<int> BoardSubcribedIds { get => User?.BoardsSubscribed; }
		#endregion

		public EntityListViewModel<BoardViewModel, GetBoardModel> BoardsModeratingListViewModel { get; private set; }
		public EntityListViewModel<BoardViewModel, GetBoardModel> BoardsSubscribedListViewModel { get; private set; }

		public EntityListViewModel<ArticleViewModel, GetArticleModel> ArticlesWrittenListViewModel { get; private set; }
		public EntityListViewModel<ArticleViewModel, GetArticleModel> ArticlesSavedListViewModel { get; private set; }

		public EntityListViewModel<CommentViewModel, GetCommentModel> CommentsWrittenListViewModel { get; private set; }
		public EntityListViewModel<CommentViewModel, GetCommentModel> CommentsSavedListViewModel { get; private set; }

		public AsyncDelegateCommand TryLoadUserCommand { get; }

		public PrivateUserViewModel(IEventAggregator ea,
			IViewManager viewManager,
			IApiClient apiClient,
			ILoadEntityCommandFactoryPrincipal loadCommandFactoryPrincipal
		)
			{
			_ea = ea;
			_viewManager = viewManager;
			_apiClient = apiClient;
			_loadCommandFactoryPrincipal = loadCommandFactoryPrincipal;
			InstantiateOwnedViewModels();

			TryLoadUserCommand = new AsyncDelegateCommand(TryLoadPrivateUserAsync);
			_ea.RegisterHandler<LoadPrivateUserMessage>(async msg => await TryLoadPrivateUserAsync());
		}

		private void InstantiateOwnedViewModels()
		{
			var loadBoardCallback = _loadCommandFactoryPrincipal.BoardCF.CreateLoadBoardCommandCallback(LoadEntityListType.LoadByIds, this);
			BoardsModeratingListViewModel = new BoardListViewModel(loadBoardCallback);
			BoardsSubscribedListViewModel = new BoardListViewModel(loadBoardCallback);

			var loadArticleCallback = _loadCommandFactoryPrincipal.ArticleCF.CreateLoadArticleCommandCallback(LoadEntityListType.LoadByIds, this);
			ArticlesWrittenListViewModel = new ArticleListViewModel(loadArticleCallback);
			ArticlesSavedListViewModel = new ArticleListViewModel(loadArticleCallback);

			var loadCommentCallbaack = _loadCommandFactoryPrincipal.CommentCF.CreateLoadCommentCommandCallback(LoadEntityListType.LoadByIds);
			CommentsWrittenListViewModel = new CommentListViewModel(loadCommentCallbaack);
			CommentsSavedListViewModel = new CommentListViewModel(loadCommentCallbaack);
		}

		private async Task TryLoadPrivateUserAsync(object parameter = null)
		{
			User = await _apiClient.GetAsync<GetPrivateUserModel>("users/me");
			if (User?.ProfileImageId > 0)
			{
				GetImageModel imgModel = await _apiClient.GetAsync<GetImageModel>(User.ProfileImageId, "images");
				BitmapImage bitmapImg = BitmapUtil.LoadImage(imgModel.ImageData);
				UserImage = bitmapImg;
			}

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
