//using Hackernews.WPF.ApiClients;
//using Hackernews.WPF.Core;
//using Hackernews.WPF.MVVM.Model;
//using HackerNews.Domain.Common.Models.Articles;
//using System.Collections.Generic;

//namespace Hackernews.WPF.MVVM.ViewModel.Articles
//{
//	public class LoadArticlePageCommand : BaseCommand
//	{
//		private readonly PaginatedListViewModel<GetArticleModel> _viewModel;
//		private readonly IApiClient _apiClient;

//		public LoadArticlePageCommand(PaginatedListViewModel<GetArticleModel> viewModel, IApiClient apiClient)
//		{
//			_viewModel = viewModel;
//			_apiClient = apiClient;
//		}

//		public override async void Execute(object parameter)
//		{
//			await App.Current.Dispatcher.Invoke(async () =>
//			{
//			});
//		}
//	}

//	public class LoadArticlePageByIdsCommand : BaseCommand
//	{
//		private readonly PaginatedListViewModel<GetArticleModel> _viewModel;
//		private readonly IApiClient _apiClient;

//		public LoadArticlePageByIdsCommand(PaginatedListViewModel<GetArticleModel> viewModel, IApiClient apiClient)
//		{
//			_viewModel = viewModel;
//			_apiClient = apiClient;
//		}

//		public override async void Execute(object parameter)
//		{
//			List<int> ids = (List<int>)parameter;

//			await App.Current.Dispatcher.Invoke(async () =>
//			{
//				_viewModel.Page = await _apiClient.GetPageAsync<GetArticleModel>(ids, _viewModel.PagingParams, "articles");
//			});
//		}
//	}
//}
