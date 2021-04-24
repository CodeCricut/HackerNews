using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories
{
	public interface ILoadArticleCommandFactory
	{
		CreateBaseCommand<EntityListViewModel<ArticleViewModel, GetArticleModel>> CreateLoadArticleCommandCallback(
			LoadEntityListType loadCommandType,
			PrivateUserViewModel userVm);
	}

	public class LoadArticleCommandFactory : ILoadArticleCommandFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly IEventAggregator _ea;

		public LoadArticleCommandFactory(IViewManager viewManager,
			IApiClient apiClient,
			IEventAggregator ea)
		{
			_viewManager = viewManager;
			_apiClient = apiClient;
			_ea = ea;
		}

		public CreateBaseCommand<EntityListViewModel<ArticleViewModel, GetArticleModel>> CreateLoadArticleCommandCallback(
			LoadEntityListType loadCommandType,
			PrivateUserViewModel userVm)
		{
			return loadCommandType switch
			{
				LoadEntityListType.LoadAll => entityListVm => new LoadArticlesCommand(entityListVm, _viewManager, userVm, _ea, _apiClient),
				_ => entityListVm => new LoadArticlesByIdsCommand(entityListVm, _viewManager, _apiClient, _ea, userVm),
			};
		}
	}
}
