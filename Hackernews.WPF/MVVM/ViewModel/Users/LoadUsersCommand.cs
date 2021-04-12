using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Users;

namespace Hackernews.WPF.MVVM.ViewModel.Users
{
	public class LoadUsersCommand : BaseCommand
	{
		private readonly UserListViewModel _viewModel;
		private readonly IApiClient _apiClient;

		public LoadUsersCommand(UserListViewModel viewModel, IApiClient apiClient)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
		}

		public override async void Execute(object parameter)
		{
			await App.Current.Dispatcher.Invoke(async () =>
			{
				_viewModel.Users.Clear();

				_viewModel.UserPageVM.Page = await _apiClient.GetPageAsync<GetPublicUserModel>(_viewModel.PagingParams, "users");

				foreach (var board in _viewModel.UserPageVM.Items)
				{
					_viewModel.Users.Add(board);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}
}
