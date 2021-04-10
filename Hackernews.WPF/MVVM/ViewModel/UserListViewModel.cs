using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class UserListViewModel : BaseViewModel
	{
		public UserListViewModel(IApiClient apiClient)
		{
			UserViewModel = new PublicUserViewModel();

			LoadCommand = new AsyncDelegateCommand(LoadUsersAsync);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, CanLoadNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, CanLoadPrevPage);
			_apiClient = apiClient;
		}

		public PublicUserViewModel UserViewModel { get;  }

		public ObservableCollection<GetPublicUserModel> Users { get; private set; } = new ObservableCollection<GetPublicUserModel>();

		private PaginatedList<GetPublicUserModel> _userPage = new PaginatedList<GetPublicUserModel>();
		private PaginatedList<GetPublicUserModel> UserPage
		{
			get => _userPage;
			set
			{
				if (_userPage != value)
				{
					_userPage = value;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(CurrentPageNumber));
					RaisePropertyChanged(nameof(TotalPages));
					RaisePropertyChanged(nameof(NumberUsers));
				}
			}
		}

		public int CurrentPageNumber { get => UserPage.PageIndex; }
		public int TotalPages { get => UserPage.TotalPages; }
		public int NumberUsers { get => UserPage.TotalCount; }

		private PagingParams _pagingParams = new PagingParams();

		#region Load Command
		public ICommand LoadCommand { get; }

		public async Task LoadUsersAsync()
		{
			Users.Clear();

			UserPage = await _apiClient.GetPageAsync<GetPublicUserModel>(_pagingParams, "users");

			foreach (var board in UserPage.Items)
			{
				Users.Add(board);
			}

			NextPageCommand.RaiseCanExecuteChanged();
			PrevPageCommand.RaiseCanExecuteChanged();

		}
		#endregion

		#region NextPage Command
		public AsyncDelegateCommand NextPageCommand { get; }

		private async Task NextPageAsync()
		{
			_pagingParams = UserPage.NextPagingParams;
			await LoadUsersAsync();

		}

		private bool CanLoadNextPage()
		{
			return UserPage.HasNextPage;
		}
		#endregion

		#region PrevPage Command
		public AsyncDelegateCommand PrevPageCommand { get; }
		public IApiClient _apiClient { get; }

		private async Task PrevPageAsync()
		{
			_pagingParams = UserPage.PreviousPagingParams;
			await LoadUsersAsync();

		}

		private bool CanLoadPrevPage()
		{
			return UserPage.HasPreviousPage;
		}
		#endregion
	}
}
