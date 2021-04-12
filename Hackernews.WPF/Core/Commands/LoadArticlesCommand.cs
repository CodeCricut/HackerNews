using Hackernews.WPF.ApiClients;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.Core.Commands
{
	public class LoadArticlesCommand : BaseCommand
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

		public override async void Execute(object parameter)
		{
			await App.Current.Dispatcher.Invoke(async () =>
			{
				_viewModel.Articles.Clear();

				_viewModel.ArticlePageVM.Page = await _apiClient.GetPageAsync<GetArticleModel>(_viewModel.PagingParams, "articles");

				_userVM.TryLoadUserCommand.Execute(null);

				foreach (var article in _viewModel.ArticlePageVM.Items)
				{
					var articleVM = new ArticleViewModel(_userVM)
					{
						Article = article
					};

					_viewModel.Articles.Add(articleVM);
				}

				_viewModel.NextPageCommand.RaiseCanExecuteChanged();
				_viewModel.PrevPageCommand.RaiseCanExecuteChanged();
			});
		}
	}
}
