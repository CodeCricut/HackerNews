using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;
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
		public IEnumerable<int> CommentIds { get => User?.CommentIds; }
		public IEnumerable<int> BoardModeratingIds { get => User?.BoardsModerating; }
		public IEnumerable<int> BoardSubcribedIds { get => User?.BoardsSubscribed; }
		#endregion

		public BoardsListViewModel BoardsModeratingListViewModel { get; private set; }
		public BoardsListViewModel BoardsSubscribedListViewModel { get; private set; }

		public AsyncDelegateCommand TryLoadUserCommand { get; }

		public PrivateUserViewModel(IApiClient apiClient)
		{
			_apiClient = apiClient;

			SetupBoardsModeratingListVM();
			SetupBoardsSubscribedListVM();

			TryLoadUserCommand = new AsyncDelegateCommand(TryLoadPrivateUserAsync);
		}

		private void SetupBoardsModeratingListVM()
		{
			BoardsModeratingListViewModel = new BoardsListViewModel(_apiClient);
			var loadModeratingBoardsCommand = new LoadBoardsByIdsCommand(BoardsModeratingListViewModel, _apiClient);
			BoardsModeratingListViewModel.LoadCommand = loadModeratingBoardsCommand;
		}

		private void SetupBoardsSubscribedListVM()
		{
			BoardsSubscribedListViewModel = new BoardsListViewModel(_apiClient);
			var loadSubscribignBoardsCommand = new LoadBoardsByIdsCommand(BoardsSubscribedListViewModel, _apiClient);
			BoardsSubscribedListViewModel.LoadCommand = loadSubscribignBoardsCommand;
		}


		private async Task TryLoadPrivateUserAsync()
		{
			User = await _apiClient.GetAsync<GetPrivateUserModel>("users/me");
			await Task.Factory.StartNew(() =>
			{
				BoardsModeratingListViewModel.LoadCommand.TryExecute(User.BoardsModerating);
				BoardsSubscribedListViewModel.LoadCommand.TryExecute(User.BoardsSubscribed);
			});
		}
	}
}
