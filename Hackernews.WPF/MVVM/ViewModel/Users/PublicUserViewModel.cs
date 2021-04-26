using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class PublicUserViewModel : BaseEntityViewModel
	{
		private bool _isSelected;
		public bool IsSelected
		{
			get { return _isSelected; }
			set { _isSelected = value; RaisePropertyChanged(); }
		}

		private GetPublicUserModel _user;
		public GetPublicUserModel User
		{
			get => _user;
			set
			{
				if (_user != value)
				{
					_user = value;
					RaisePropertyChanged("");
				}
			}
		}

		private readonly IApiClient _apiClient;

		private BitmapImage _userImage;
		public BitmapImage UserImage
		{
			get { return _userImage; }
			set { _userImage = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(HasImage)); }
		}

		private async Task LoadUserAsync(object parameter = null)
		{
			if (User?.ProfileImageId > 0)
			{
				GetImageModel imgModel = await _apiClient.GetAsync<GetImageModel>(User.ProfileImageId, "images");
				BitmapImage bitmapImg = BitmapUtil.LoadImage(imgModel.ImageData);
				UserImage = bitmapImg;
			}
		}

		public PublicUserViewModel(IApiClient apiClient)
		{
			_apiClient = apiClient;
			LoadEntityCommand = new AsyncDelegateCommand(LoadUserAsync);
		}


		public bool HasImage { get => UserImage != null; }

		public string Username
		{
			get => User?.Username ?? "";
			set
			{
				if (!User?.Username.Equals(value) ?? false)
				{
					User.Username = value;
					RaisePropertyChanged();
				}
			}
		}

		public int Karma
		{
			get => User?.Karma ?? 0;
			set
			{
				if (User?.Karma != value)
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
			get => User?.JoinDate ?? new DateTimeOffset(JoinDate);
			set
			{
				if (User?.JoinDate != value.DateTime)
				{
					User.JoinDate = value.DateTime;
					RaisePropertyChanged();
				}
			}
		}
	}
}
