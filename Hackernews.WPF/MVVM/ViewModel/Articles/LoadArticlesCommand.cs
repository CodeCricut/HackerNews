using Hackernews.WPF.ApiClients;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.Core.Commands
{
	public class LoadArticlesByIdsCommand : LoadEntityListByIdsCommand<ArticleViewModel, GetArticleModel>
	{
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVm;

		public LoadArticlesByIdsCommand(EntityListViewModel<ArticleViewModel, GetArticleModel> listVM,
			IApiClient apiClient,
			PrivateUserViewModel userVm) : base(listVM)
		{
			_apiClient = apiClient;
			_userVm = userVm;
		}

		public override ArticleViewModel ConstructEntityViewModel(GetArticleModel getModel)
		{
			return new ArticleViewModel(_userVm, _apiClient) { Article = getModel };
		}

		public override Task<PaginatedList<GetArticleModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
		{
			return _apiClient.GetAsync<GetArticleModel>(ids, pagingParams, "articles");
		}
	}

	public class LoadArticlesCommand : LoadEntityListCommand<ArticleViewModel, GetArticleModel>
	{
		private readonly PrivateUserViewModel _userVM;
		private readonly IApiClient _apiClient;

		public LoadArticlesCommand(EntityListViewModel<ArticleViewModel, GetArticleModel> listVM,
			PrivateUserViewModel userVM,
			IApiClient apiClient) : base(listVM)
		{
			_userVM = userVM;
			_apiClient = apiClient;
		}

		public override ArticleViewModel ConstructEntityViewModel(GetArticleModel getModel)
		{
			return new ArticleViewModel(_userVM, _apiClient) { Article = getModel };
		}

		public override Task<PaginatedList<GetArticleModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _apiClient.GetPageAsync<GetArticleModel>(pagingParams, "articles");
		}
	}
}
