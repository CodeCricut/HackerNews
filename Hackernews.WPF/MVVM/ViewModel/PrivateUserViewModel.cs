using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.ViewModels
{
	public class PrivateUserViewModel : BaseViewModel
	{
		private readonly IApiClient _apiClient;

		private GetPrivateUserModel _user;
		private GetPrivateUserModel User
		{
			get => _user;
			set { 
				if (_user != value)
				{
					_user = value;
					RaisePropertyChanged("");
				}
			}
		}

		public bool IsLoaded { get => User != null; }

		public AsyncDelegateCommand TryLoadUserCommand { get; }

		public PrivateUserViewModel(IApiClient apiClient)
		{
			_apiClient = apiClient;
			User = new GetPrivateUserModel();
			TryLoadUserCommand = new AsyncDelegateCommand(TryLoadPrivateUser);
		}

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
		public IEnumerable<int> CommentIds { get => User?.CommentIds; }
		public IEnumerable<int> BoardModeratingIds { get => User?.BoardsModerating; }
		public IEnumerable<int> BoardSubcribedIds { get => User?.BoardsSubscribed; }

		private async Task TryLoadPrivateUser()
		{
			try
			{
				User = await _apiClient.GetAsync<GetPrivateUserModel>("users/me");
			}
			catch (Exception)
			{
			}
		}
	}
}
