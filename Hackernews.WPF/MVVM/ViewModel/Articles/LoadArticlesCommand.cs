using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.WPF.MessageBus.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.Core.Commands
{
	public class LoadArticlesByIdsCommand : LoadEntityListByIdsCommand<ArticleViewModel, GetArticleModel>
	{
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly IEventAggregator _ea;
		private readonly PrivateUserViewModel _userVm;

		public LoadArticlesByIdsCommand(EntityListViewModel<ArticleViewModel, GetArticleModel> listVM,
			IViewManager viewManager,
			IApiClient apiClient,
			IEventAggregator ea,
			PrivateUserViewModel userVm) : base(listVM)
		{
			_viewManager = viewManager;
			_apiClient = apiClient;
			_ea = ea;
			_userVm = userVm;
		}

		public override ArticleViewModel ConstructEntityViewModel(GetArticleModel getModel)
		{
			return new ArticleViewModel(_ea, _viewManager, _userVm, _apiClient) { Article = getModel };
		}

		public override Task<PaginatedList<GetArticleModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
		{
			return _apiClient.GetAsync<GetArticleModel>(ids, pagingParams, "articles");
		}
	}

	public class LoadArticlesCommand : LoadEntityListCommand<ArticleViewModel, GetArticleModel>
	{
		private readonly IViewManager _viewManager;
		private readonly PrivateUserViewModel _userVM;
		private readonly IEventAggregator _ea;
		private readonly IApiClient _apiClient;

		public LoadArticlesCommand(EntityListViewModel<ArticleViewModel, GetArticleModel> listVM,
			IViewManager viewManager,
			PrivateUserViewModel userVM,
			IEventAggregator ea,
			IApiClient apiClient) : base(listVM)
		{
			_viewManager = viewManager;
			_userVM = userVM;
			_ea = ea;
			_apiClient = apiClient;
		}

		public override ArticleViewModel ConstructEntityViewModel(GetArticleModel getModel)
		{
			return new ArticleViewModel(_ea, _viewManager, _userVM, _apiClient) { Article = getModel };
		}

		public override Task<PaginatedList<GetArticleModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _apiClient.GetPageAsync<GetArticleModel>(pagingParams, "articles");
		}
	}
}
