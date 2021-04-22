using Hackernews.WPF.ApiClients;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Articles;
using System.Collections.Generic;

namespace Hackernews.WPF.Core.Commands
{
	public abstract class BaseLoadArticlesCommand : BaseCommand
	{
	}

	public class LoadArticlesByIdsCommand : BaseLoadArticlesCommand
	{
		private readonly ArticleListViewModel _viewModel;
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _privateUserViewModel;

		public LoadArticlesByIdsCommand(ArticleListViewModel viewModel, IApiClient apiClient, PrivateUserViewModel privateUserViewModel)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
			_privateUserViewModel = privateUserViewModel;
		}

		public override async void Execute(object parameter)
		{
			List<int> ids = (List<int>)parameter;
			await App.Current.Dispatcher.Invoke(async () =>
			{

				_viewModel.Articles.Clear();

				_viewModel.ArticlePageVM.Page = await _apiClient.GetAsync<GetArticleModel>(ids, _viewModel.ArticlePageVM.PagingParams, "articles");

				foreach (var article in _viewModel.ArticlePageVM.Items)
				{
					var vm = new ArticleViewModel(_privateUserViewModel, _apiClient)
					{
						Article = article
					};
					vm.LoadEntityCommand.Execute();
					_viewModel.Articles.Add(vm);
				}
			});
		}
	}

	public class LoadArticlesCommand : BaseLoadArticlesCommand
	{
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVM;
		private readonly ArticleListViewModel _viewModel;

		public LoadArticlesCommand(ArticleListViewModel viewModel,
			IApiClient apiClient,
			PrivateUserViewModel userVM)
		{
			_viewModel = viewModel;
			_apiClient = apiClient;
			_userVM = userVM;
		}

		public override  async void Execute(object parameter)
		{
			await App.Current.Dispatcher.Invoke(async () =>
			{
				_viewModel.Articles.Clear();
				_viewModel.ArticlePageVM.Page = await _apiClient.GetPageAsync<GetArticleModel>(_viewModel.ArticlePageVM.PagingParams, "articles");

				_userVM.TryLoadUserCommand.Execute(null);

				foreach (var article in _viewModel.ArticlePageVM.Items)
				{
					var articleVM = new ArticleViewModel(_userVM, _apiClient)
					{
						Article = article
					};
					articleVM.LoadEntityCommand.Execute();

					_viewModel.Articles.Add(articleVM);
				}
			});
		}
	}
}
