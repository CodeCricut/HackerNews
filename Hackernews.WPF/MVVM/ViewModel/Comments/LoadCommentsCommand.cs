using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using HackerNews.Domain.Common.Models.Comments;
using System.Collections.Generic;

namespace Hackernews.WPF.MVVM.ViewModel.Comments
{
	public abstract class BaseLoadCommentsCommand : BaseCommand
	{

	}

	public class LoadCommentsByIdsCommand : BaseLoadCommentsCommand
	{
		private readonly CommentListViewModel _viewModel;
		private readonly IApiClient _apiClient;

		public LoadCommentsByIdsCommand(CommentListViewModel viewModel, IApiClient apiClient)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
		}

		public override async void Execute(object parameter)
		{
			List<int> ids = (List<int>)parameter;
			await App.Current.Dispatcher.Invoke(async () =>
			{

				_viewModel.Comments.Clear();

				_viewModel.CommentPageVM.Page = await _apiClient.GetAsync<GetCommentModel>(ids, _viewModel.PagingParams, "comments");

				foreach (var comment in _viewModel.CommentPageVM.Items)
				{
					var vm = new CommentViewModel()
					{
						Comment = comment
					};
					_viewModel.Comments.Add(vm);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}


	public class LoadCommentsCommand : BaseLoadCommentsCommand
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

				foreach (var comment in _viewModel.CommentPageVM.Items)
				{
					var vm = new CommentViewModel()
					{
						Comment = comment
					};
					_viewModel.Comments.Add(vm);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}
}
