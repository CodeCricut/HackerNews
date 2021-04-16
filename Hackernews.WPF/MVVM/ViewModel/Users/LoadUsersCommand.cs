using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using HackerNews.Domain.Common.Models.Users;

namespace Hackernews.WPF.MVVM.ViewModel
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
			await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
			{
				_viewModel.Users.Clear();

				_viewModel.UserPageVM.Page = await _apiClient.GetPageAsync<GetPublicUserModel>(_viewModel.PagingParams, "users");

				foreach (var user in _viewModel.UserPageVM.Items)
				{

					var vm = new PublicUserViewModel(_apiClient)
					{
						User = user
					};
					vm.LoadUserCommand.Execute();
					_viewModel.Users.Add(vm);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}
}
