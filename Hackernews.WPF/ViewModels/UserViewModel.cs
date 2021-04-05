using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Threading.Tasks;

namespace Hackernews.WPF.ViewModels
{
	public class UserViewModel : BaseViewModel
	{
		private readonly IApiClient _apiClient;

		private GetPrivateUserModel _user;

		public GetPrivateUserModel User
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

		

		public AsyncDelegateCommand TryLoadUserCommand { get; }

		public UserViewModel(IApiClient apiClient)
		{
			_apiClient = apiClient;
			User = new GetPrivateUserModel();
			TryLoadUserCommand = new AsyncDelegateCommand(TryLoadPrivateUser);
		}

		public string UserName
		{ 
			get => User.UserName;
			set
			{
				if (!User.UserName.Equals(value))
				{
					User.UserName = value;
					RaisePropertyChanged();
				}
			} 
		}

		public int Karma
		{
			get => User.Karma;
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
			get => User.JoinDate;
			set
			{
				if (User.JoinDate != value)
				{
					User.JoinDate = value;
					RaisePropertyChanged();
				}
			}
		}

		public DateTimeOffset JoinDateOffset
		{
			get => User.JoinDate;
			set
			{
				if (User.JoinDate != value.DateTime)
				{
					User.JoinDate = value.DateTime;
					RaisePropertyChanged();
				}
			}
		}

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
