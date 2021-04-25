using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.WPF.MessageBus.Core;

namespace Hackernews.WPF.Factories
{
	public interface ILoadCommentCommandFactory
	{
		CreateBaseCommand<EntityListViewModel<CommentViewModel, GetCommentModel>> CreateLoadCommentCommandCallback(LoadEntityListType loadCommandType);
	}

	public class LoadCommentCommandFactory : ILoadCommentCommandFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly IEventAggregator _ea;

		public LoadCommentCommandFactory(IViewManager viewManager,
			IApiClient apiClient,
			IEventAggregator ea)
		{
			_viewManager = viewManager;
			_apiClient = apiClient;
			_ea = ea;
		}

		public CreateBaseCommand<EntityListViewModel<CommentViewModel, GetCommentModel>> CreateLoadCommentCommandCallback(LoadEntityListType loadCommandType)
		{
			return loadCommandType switch
			{
				LoadEntityListType.LoadAll => entityListVm => new LoadCommentsCommand(entityListVm, _apiClient),
				_ => entityListVm => new LoadCommentsByIdsCommand(entityListVm, _apiClient),
			};
		}
	}
}
