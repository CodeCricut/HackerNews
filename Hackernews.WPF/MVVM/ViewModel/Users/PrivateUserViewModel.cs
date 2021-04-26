using Hackernews.WPF.Factories;
using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using HackerNews.ApiConsumer.Account;
using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.Images;
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
		private readonly IPrivateUserApiClient _privateUserApiClient;
		private readonly IImageApiClient _imageApiClient;

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

		public EntityListViewModel<BoardViewModel, GetBoardModel> BoardsModeratingListViewModel { get; }
		public EntityListViewModel<BoardViewModel, GetBoardModel> BoardsSubscribedListViewModel { get;  }

		public EntityListViewModel<ArticleViewModel, GetArticleModel> ArticlesWrittenListViewModel { get;  }
		public EntityListViewModel<ArticleViewModel, GetArticleModel> ArticlesSavedListViewModel { get; }

		public EntityListViewModel<CommentViewModel, GetCommentModel> CommentsWrittenListViewModel { get; }
		public EntityListViewModel<CommentViewModel, GetCommentModel> CommentsSavedListViewModel { get; }

		public AsyncDelegateCommand TryLoadUserCommand { get; }

		public PrivateUserViewModel(IEventAggregator ea,
			IViewManager viewManager,
			IPrivateUserApiClient privateUserApiClient,
			IImageApiClient imageApiClient,
			
			IBoardListViewModelFactory boardListViewModelFactory,
			IArticleListViewModelFactory articleListViewModelFactory,
			ICommentListViewModelFactory commentListViewModelFactory
		)
		{
			_ea = ea;
			_viewManager = viewManager;
			_privateUserApiClient = privateUserApiClient;
			_imageApiClient = imageApiClient;

			BoardsModeratingListViewModel = boardListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			BoardsSubscribedListViewModel = boardListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);

			ArticlesWrittenListViewModel = articleListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			ArticlesSavedListViewModel = articleListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			CommentsWrittenListViewModel = commentListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			CommentsSavedListViewModel = commentListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);

			TryLoadUserCommand = new AsyncDelegateCommand(TryLoadPrivateUserAsync);
			_ea.RegisterHandler<LoadPrivateUserMessage>(async msg => await TryLoadPrivateUserAsync());
		}

		private async Task TryLoadPrivateUserAsync(object parameter = null)
		{
			User = await _privateUserApiClient.GetAsync();
			if (User?.ProfileImageId > 0)
			{
				GetImageModel imgModel = await _imageApiClient.GetImageAsync(User.ProfileImageId);
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
