using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Boards;
using System.Collections.Generic;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public abstract class BaseLoadBoardsCommand : BaseCommand
	{

	}

	public class LoadBoardsByIdsCommand : BaseLoadBoardsCommand
	{
		private readonly BoardsListViewModel _viewModel;
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVM;

		public LoadBoardsByIdsCommand(BoardsListViewModel viewModel, IApiClient apiClient, PrivateUserViewModel userVM)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
			_userVM = userVM;
		}

		public override async void Execute(object parameter)
		{
			List<int> ids = (List<int>)parameter;
			await App.Current.Dispatcher.Invoke(async () =>
			{

				_viewModel.Boards.Clear();

				_viewModel.BoardPageVM.Page = await _apiClient.GetAsync<GetBoardModel>(ids, _viewModel.PagingParams, "boards");

				foreach (var board in _viewModel.BoardPageVM.Items)
				{
					var vm = new BoardViewModel(_apiClient, _userVM)
					{
						Board = board
					};

					// TODO: idk if this is where it should be done, at the very least run asynchronously
					vm.LoadBoardCommand.Execute();

					_viewModel.Boards.Add(vm);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}

	public class LoadBoardsCommand : BaseLoadBoardsCommand
	{
		private readonly BoardsListViewModel _viewModel;
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVM;

		public LoadBoardsCommand(BoardsListViewModel viewModel,
			IApiClient apiClient, PrivateUserViewModel userVM)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
			_userVM = userVM;
		}

		public override async void Execute(object parameter)
		{
			await App.Current.Dispatcher.Invoke(async () =>
			{

				_viewModel.Boards.Clear();

				_viewModel.BoardPageVM.Page = await _apiClient.GetPageAsync<GetBoardModel>(_viewModel.PagingParams, "boards");

				foreach (var board in _viewModel.BoardPageVM.Items)
				{
					var vm = new BoardViewModel(_apiClient, _userVM)
					{
						Board = board
					};

					// TODO: idk if this is where it should be done, at the very least run asynchronously
					vm.LoadBoardCommand.Execute();

					_viewModel.Boards.Add(vm);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}
}
