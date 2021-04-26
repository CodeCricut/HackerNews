using Hackernews.WPF.Core;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.Commands
{
	public interface ILoadArticlesCommandFactory
	{
		BaseCommand Create(EntityListViewModel<ArticleViewModel, GetArticleModel> userListVm, LoadEntityListEnum loadEntityType);
	}

	public class LoadArticlesCommandFactory : ILoadArticlesCommandFactory
	{
		private readonly IArticleApiClient _articleApiClient;
		private readonly IArticleViewModelFactory _articleViewModelFactory;

		public LoadArticlesCommandFactory(
			IArticleApiClient articleApiClient,
			IArticleViewModelFactory articleViewModelFactory)
		{
			_articleApiClient = articleApiClient;
			_articleViewModelFactory = articleViewModelFactory;
		}

		public BaseCommand Create(EntityListViewModel<ArticleViewModel, GetArticleModel> articleListVm, LoadEntityListEnum loadEntityType)
		{
			return loadEntityType switch
			{
				LoadEntityListEnum.LoadAll => new LoadArticlesCommand(articleListVm, _articleApiClient, _articleViewModelFactory),
				_ => new LoadArticlesByIdsCommand(articleListVm, _articleApiClient, _articleViewModelFactory),
			};
		}
	}
}
