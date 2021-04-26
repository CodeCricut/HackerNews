using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.WPF.MessageBus.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.Core.Commands
{
	public class LoadArticlesByIdsCommand : LoadEntityListByIdsCommand<ArticleViewModel, GetArticleModel>
	{
		private readonly IArticleApiClient _articleApiClient;
		private readonly IArticleViewModelFactory _articleViewModelFactory;

		public LoadArticlesByIdsCommand(EntityListViewModel<ArticleViewModel, GetArticleModel> listVM,
			IArticleApiClient articleApiClient,
			IArticleViewModelFactory articleViewModelFactory) : base(listVM)
		{
			_articleApiClient = articleApiClient;
			_articleViewModelFactory = articleViewModelFactory;
		}

		public override ArticleViewModel ConstructEntityViewModel(GetArticleModel getModel)
		{
			var articleVm = _articleViewModelFactory.Create();
			articleVm.Article = getModel;
			return articleVm;
		}

		public override Task<PaginatedList<GetArticleModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
		{
			return _articleApiClient.GetByIdsAsync(ids, pagingParams);
		}
	}

	public class LoadArticlesCommand : LoadEntityListCommand<ArticleViewModel, GetArticleModel>
	{
		private readonly IArticleApiClient _articleApiClient;
		private readonly IArticleViewModelFactory _articleViewModelFactory;

		public LoadArticlesCommand(EntityListViewModel<ArticleViewModel, GetArticleModel> listVM,
			IArticleApiClient articleApiClient,
			IArticleViewModelFactory articleViewModelFactory
			) : base(listVM)
		{
			_articleApiClient = articleApiClient;
			_articleViewModelFactory = articleViewModelFactory;
		}

		public override ArticleViewModel ConstructEntityViewModel(GetArticleModel getModel)
		{
			var articleVm = _articleViewModelFactory.Create();
			articleVm.Article = getModel;
			return articleVm;
		}

		public override Task<PaginatedList<GetArticleModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _articleApiClient.GetPageAsync(pagingParams);
		}
	}
}
