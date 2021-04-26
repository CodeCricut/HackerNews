using Hackernews.WPF.Helpers;
using HackerNews.ApiConsumer.Account;
using HackerNews.ApiConsumer.Images;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hackernews.WPF.ViewModels
{
	public class PrivateUserViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IPrivateUserApiClient _privateUserApiClient;
		private readonly IImageApiClient _imageApiClient;

		#region User and User Props
		private GetPrivateUserModel _user = new GetPrivateUserModel();
		public GetPrivateUserModel User
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


		public PrivateUserViewModel(IEventAggregator ea,
			IPrivateUserApiClient privateUserApiClient,
			IImageApiClient imageApiClient
		)
		{
			_ea = ea;
			_privateUserApiClient = privateUserApiClient;
			_imageApiClient = imageApiClient;
		}

		public async Task LoadUserAsync()
		{
			User = await _privateUserApiClient.GetAsync();
			if (User?.ProfileImageId > 0)
			{
				GetImageModel imgModel = await _imageApiClient.GetImageAsync(User.ProfileImageId);
				BitmapImage bitmapImg = BitmapUtil.LoadImage(imgModel.ImageData);
				UserImage = bitmapImg;
			}
		}

	}
}
