using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using HackerNews.Domain.Common.Models.Comments;

namespace Hackernews.WPF.MVVM.ViewModel.Comments
{
	public class LoadCommentsCommand : BaseCommand
	{
		private readonly CommentListViewModel _viewModel;
		private readonly IApiClient _apiClient;

		public LoadCommentsCommand(CommentListViewModel viewModel,
			IApiClient apiClient)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
		}

		public override async void Execute(object parameter)
		{
			await App.Current.Dispatcher.Invoke(async () =>
			{
				_viewModel.Comments.Clear();

				_viewModel.CommentPageVM.Page = await _apiClient.GetPageAsync<GetCommentModel>(_viewModel.PagingParams, "comments");

				foreach (var board in _viewModel.CommentPageVM.Items)
				{
					_viewModel.Comments.Add(board);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}
}
