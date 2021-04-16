﻿using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
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

		public LoadBoardsByIdsCommand(BoardsListViewModel viewModel, IApiClient apiClient)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
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
					var vm = new BoardViewModel(_apiClient)
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

		public LoadBoardsCommand(BoardsListViewModel viewModel,
			IApiClient apiClient)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
		}

		public override async void Execute(object parameter)
		{
			await App.Current.Dispatcher.Invoke(async () =>
			{

				_viewModel.Boards.Clear();

				_viewModel.BoardPageVM.Page = await _apiClient.GetPageAsync<GetBoardModel>(_viewModel.PagingParams, "boards");

				foreach (var board in _viewModel.BoardPageVM.Items)
				{
					var vm = new BoardViewModel(_apiClient)
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