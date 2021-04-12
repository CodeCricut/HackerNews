using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Boards;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class LoadBoardsCommand : BaseCommand
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
					_viewModel.Boards.Add(board);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}
}
