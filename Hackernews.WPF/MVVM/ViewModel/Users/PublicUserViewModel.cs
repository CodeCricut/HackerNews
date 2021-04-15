using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Users;
using System;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class PublicUserViewModel : BaseViewModel
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

		public string UserName
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
