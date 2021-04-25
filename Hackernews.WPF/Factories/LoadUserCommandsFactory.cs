using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.WPF.MessageBus.Core;

namespace Hackernews.WPF.Factories
{
	public interface ILoadUserCommandFactory
	{
		CreateBaseCommand<EntityListViewModel<PublicUserViewModel, GetPublicUserModel>> CreateLoadUserCommandCallback(LoadEntityListType loadCommandType);
	}

	public class LoadUserCommandFactory : ILoadUserCommandFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly IEventAggregator _ea;

		public LoadUserCommandFactory(IViewManager viewManager,
			IApiClient apiClient,
			IEventAggregator ea)
		{
			_viewManager = viewManager;
			_apiClient = apiClient;
			_ea = ea;
		}

		public CreateBaseCommand<EntityListViewModel<PublicUserViewModel, GetPublicUserModel>> CreateLoadUserCommandCallback(LoadEntityListType loadCommandType)
		{
			return loadCommandType switch
			{
				LoadEntityListType.LoadAll => entityListVm => new LoadUsersCommand(entityListVm, _apiClient),
				_ => entityListVm => new LoadUsersByIdsCommand(entityListVm, _apiClient),
			};
		}
	}
}
