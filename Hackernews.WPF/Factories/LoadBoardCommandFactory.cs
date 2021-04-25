using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.WPF.MessageBus.Core;

namespace Hackernews.WPF.Factories
{
	public interface ILoadBoardCommandFactory
	{
		CreateBaseCommand<EntityListViewModel<BoardViewModel, GetBoardModel>> CreateLoadBoardCommandCallback(
			LoadEntityListType loadCommandType,
			PrivateUserViewModel userVm);
	}

	public class LoadBoardCommandFactory : ILoadBoardCommandFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly IEventAggregator _ea;

		public LoadBoardCommandFactory(IViewManager viewManager,
			IApiClient apiClient,
			IEventAggregator ea)
		{
			_viewManager = viewManager;
			_apiClient = apiClient;
			_ea = ea;
		}

		public CreateBaseCommand<EntityListViewModel<BoardViewModel, GetBoardModel>> CreateLoadBoardCommandCallback(
			LoadEntityListType loadCommandType,
			PrivateUserViewModel userVm)
		{
			return loadCommandType switch
			{
				LoadEntityListType.LoadAll => entityListVm => new LoadBoardsCommand(entityListVm, _viewManager, _apiClient, _ea, userVm),
				_ => entityListVm => new LoadBoardsByIdsCommand(entityListVm, _viewManager, _apiClient, _ea, userVm),
			};
		}
	}
}
